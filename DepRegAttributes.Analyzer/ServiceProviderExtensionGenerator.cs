using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace DepRegAttributes.Analyzer;

[Generator]
public class ServiceProviderExtensionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (c, _) => c is ClassDeclarationSyntax cd &&
                cd != null &&
                cd.AttributeLists.Any(),
            transform: (context, _) => Transform(context))
                .Where(static m => m != null);

        var complation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(complation,
            (spc, source) => Execute(spc, source.Left, source.Right));
    }

    private ((string @namespace, string name) Implementation, IEnumerable<(string Lifetime, IEnumerable<(string @namespace, string name)> Services, (string @namespace, string name) Tag, (string @namespace, string name) Key)>)? Transform(
        GeneratorSyntaxContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classDeclaration)
            return null;

        var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);

        if (symbol is not INamedTypeSymbol implementation)
            return null;

        if (implementation.IsAbstract || implementation.IsStatic || implementation.DeclaredAccessibility is not (Accessibility.Public or Accessibility.Internal))
            return null;

        if (!implementation.Constructors.Any(c => c.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal))
            return null;

        var registrationAttributes = classDeclaration.AttributeLists
            .SelectMany(a => a.Attributes)
            .Where(a => Helpers.IsRegistrationAttribute(a, context.SemanticModel))
            .Select(a => (Helpers.GetAttributeName(a).Replace("Register", "").Replace("Attribute", ""),
                GetServiceNames(a, context.SemanticModel, implementation), 
                Helpers.GetPropertyArgument(a, context.SemanticModel, "Tag"),
                Helpers.GetPropertyArgument(a, context.SemanticModel, "Key")));

        return (Helpers.GetNamespaceAndName(implementation), registrationAttributes);
    }

    private void Execute(
        SourceProductionContext context,
        Compilation complation,
        ImmutableArray<((string @namespace, string name) Implementation, IEnumerable<(string Lifetime, IEnumerable<(string @namespace, string name)> Services, (string @namespace, string name) Tag, (string @namespace, string name) Key)> Attributes)?> classes)
    {
        if (!complation.ReferencedAssemblyNames.Any(r => 
            r.Name == "Microsoft.Extensions.DependencyInjection" && r.Version >= new Version(3, 1, 32)))
            return;

        var taggedRegistrations = new Dictionary<string, Dictionary<string, List<(string Imp, IEnumerable<string> Services, string Key)>>>();
        var untaggedRegistrations = new Dictionary<string, List<(string Imp, IEnumerable<string> Services, string Key)>>();
        var namespaces = new List<string>();


        try
        {
            foreach (var registration in classes.Where(r => r.HasValue))
            {
                namespaces.Add(registration.Value.Implementation.@namespace);
                foreach (var (Lifetime, Services, Tag, Key) in registration.Value.Attributes)
                {
                    if (string.IsNullOrEmpty(Tag.name))
                    {
                        if (!untaggedRegistrations.ContainsKey(Lifetime))
                            untaggedRegistrations.Add(Lifetime, []);
                        untaggedRegistrations[Lifetime].Add((registration.Value.Implementation.name, Services.Select(s => s.name), Key.name));
                        namespaces.AddRange(Services.Select(s => s.@namespace));
                        namespaces.Add(Key.@namespace);
                    }
                    else
                    {
                        if (!taggedRegistrations.ContainsKey(Tag.name))
                            taggedRegistrations.Add(Tag.name, []);
                        if (!taggedRegistrations[Tag.name].ContainsKey(Lifetime))
                            taggedRegistrations[Tag.name].Add(Lifetime, []);
                        taggedRegistrations[Tag.name][Lifetime].Add((registration.Value.Implementation.name, Services.Select(s => s.name), Key.name));
                        namespaces.AddRange(Services.Select(s => s.@namespace));
                        namespaces.Add(Key.@namespace);
                        namespaces.Add(Tag.@namespace);
                    }
                }
            }

            var fileContentBuilder = new StringBuilder();
            fileContentBuilder.AppendLine("//Auto Generated File");
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            foreach (var @namespace in namespaces.Distinct().Where(s => !string.IsNullOrEmpty(s)))
            {
                fileContentBuilder.AppendLine($"using {@namespace};");
            }
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine($"namespace {complation.GetLibraryNamespace()}");
            fileContentBuilder.AppendLine("{");
            fileContentBuilder.AppendLine("    public static class ServiceProviderExtensions");
            fileContentBuilder.AppendLine("    {");

            fileContentBuilder.AppendLine("        public static IServiceCollection RegisterDependenciesByAttribute(this IServiceCollection services, params object[] includeTags)");
            fileContentBuilder.AppendLine("        {");
            fileContentBuilder.AppendLine("            return services.AddByAttribute(includeTags);");
            fileContentBuilder.AppendLine("        }");
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("        public static IServiceCollection AddByAttribute(this IServiceCollection services, params object[] includeTags)");
            fileContentBuilder.AppendLine("        {");

            fileContentBuilder.Append(GetServiceRegistrations(untaggedRegistrations, 12));

            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("            foreach (object includedTag in includeTags)");
            fileContentBuilder.AppendLine("            {");
            fileContentBuilder.AppendLine("                if(includedTag == null)");
            fileContentBuilder.AppendLine("                {");
            fileContentBuilder.AppendLine("                    continue;");
            fileContentBuilder.AppendLine("                }");

            foreach (var taggedRegistration in taggedRegistrations)
            {
                fileContentBuilder.AppendLine();
                fileContentBuilder.AppendLine($"                if(includedTag.Equals({taggedRegistration.Key}))");
                fileContentBuilder.AppendLine("                {");

                fileContentBuilder.Append(GetServiceRegistrations(taggedRegistration.Value, 20));

                fileContentBuilder.AppendLine("                }");
            }

            fileContentBuilder.AppendLine("            }");

            fileContentBuilder.AppendLine($"            return services;");
            fileContentBuilder.AppendLine("        }");
            fileContentBuilder.AppendLine("    }");
            fileContentBuilder.AppendLine("}");

            context.AddSource($"ServiceProviderExtensions.g.cs", fileContentBuilder.ToString());
        }
        catch (Exception e)
        {
            context.AddSource($"Error.g.cs", e.ToString());
            return;
        }
    }

    private string GetServiceRegistrations(Dictionary<string, List<(string Implementation, IEnumerable<string> Services, string Key)>> services, int indent = 0)
    {
        var fileContentBuilder = new StringBuilder();

        foreach (var registration in services)
        {
            foreach (var (Implementation, Services, Key) in registration.Value)
            {
                if (string.IsNullOrEmpty(Key))
                {
                    if (!Services.Any())
                    {
                        fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}<{Implementation}>();", indent);
                    }
                    else
                    {
                        fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}<{Services.First()}, {Implementation}>();", indent);

                        foreach (var service in Services.Skip(1))
                        {
                            if (registration.Key == Consts.Transient)
                                fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}<{service}, {Implementation}>();", indent);
                            else
                                fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}(sp => ({service})sp.GetRequiredService<{Services.First()}>());", indent);
                        }
                    }
                }
                else
                {
                    if (!Services.Any())
                    {
                        fileContentBuilder.AppendLineIndented($"services.AddKeyed{registration.Key}<{Implementation}>({Key});", indent);
                    }
                    else
                    {
                        fileContentBuilder.AppendLineIndented($"services.AddKeyed{registration.Key}<{Services.First()}, {Implementation}>({Key});", indent);

                        foreach (var service in Services.Skip(1))
                        {
                            if (registration.Key == Consts.Transient)
                                fileContentBuilder.AppendLineIndented($"services.AddKeyed{registration.Key}<{service}, {Implementation}>({Key});", indent);
                            else
                                fileContentBuilder.AppendLineIndented($"services.AddKeyed{registration.Key}({Key}, (sp, key) => ({service})sp.GetRequiredKeyedService<{Services.First()}>(key));", indent);
                        }
                    }
                }
            }
        }

        return fileContentBuilder.ToString();
    }

    private IEnumerable<(string, string)> GetServiceNames(AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
        => Helpers.GetServices(attributeSyntax, semanticModel, implementation)
            .Where(s => Helpers.IsTypeInHierarchy(s, implementation) && s.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
            .Select(Helpers.GetNamespaceAndName);

}

public static class StringBuilderExtensions
{
    public static StringBuilder AppendLineIndented(this StringBuilder sb, string textBlock, int indent)
    {
        var indentText = new string(' ', indent); 

        foreach (var line in textBlock.TrimEnd().Split('\n'))
            if (!string.IsNullOrWhiteSpace(line))
                sb.AppendLine($"{indentText}{line}");
        return sb;
    }
}
