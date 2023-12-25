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

    private (string Implementation, IEnumerable<(string Lifetime, IEnumerable<string> Services, string Tag)>)? Transform(
        GeneratorSyntaxContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classDeclaration)
            return null;

        var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);

        if(symbol is not INamedTypeSymbol implementation)
            return null;

        if (implementation.IsAbstract || implementation.IsStatic)
            return null;

        if (!implementation.Constructors.Any(c => c.DeclaredAccessibility == Accessibility.Public))
            return null;

        var registrationAttributes = classDeclaration.AttributeLists
            .SelectMany(a => a.Attributes)
            .Where(a => Helpers.IsRegistrationAttribute(a, context.SemanticModel))
            .Select(a => (Helpers.GetAttributeName(a).Replace("Register", "").Replace("Attribute", ""),
                GetServiceNames(a, context.SemanticModel, implementation), GetTag(a)));

        return (Helpers.GetFullName(implementation), registrationAttributes);
    }

    private string GetTag(AttributeSyntax attributeSyntax)
    {
        if(attributeSyntax.ArgumentList is null)
            return string.Empty;

        var taggedAttribute = attributeSyntax.ArgumentList.Arguments
            .FirstOrDefault(a => a.NameEquals is not null && 
                                 a.NameEquals.Name.Identifier.Text == "Tag" && 
                                 a.Expression is LiteralExpressionSyntax);

        return taggedAttribute is null ? string.Empty : (taggedAttribute.Expression as LiteralExpressionSyntax).Token.Value.ToString();
    }

    private void Execute(
        SourceProductionContext context,
        Compilation complation,
        ImmutableArray<(string Implementation, IEnumerable<(string Lifetime, IEnumerable<string> Services, string Tag)> Attributes)?> classes)
    {
        if (!complation.ReferencedAssemblyNames.Any(r => r.Name == "Microsoft.Extensions.DependencyInjection"))
            return;

        var taggedRegistrations = new Dictionary<string, Dictionary<string, List<(string, IEnumerable<string>)>>>();
        var untaggedRegistrations = new Dictionary<string, List<(string, IEnumerable<string>)>>();

        try
        {
            foreach (var registration in classes.Where(r => r.HasValue))
            {
                foreach (var (Lifetime, Services, Tag) in registration.Value.Attributes)
                {
                    if (string.IsNullOrEmpty(Tag))
                    {
                        if (!untaggedRegistrations.ContainsKey(Lifetime))
                            untaggedRegistrations.Add(Lifetime, []);
                        untaggedRegistrations[Lifetime].Add((registration.Value.Implementation, Services));
                    }
                    else
                    {
                        if (!taggedRegistrations.ContainsKey(Tag))
                            taggedRegistrations.Add(Tag, []);
                        if (!taggedRegistrations[Tag].ContainsKey(Lifetime))
                            taggedRegistrations[Tag].Add(Lifetime, []);
                        taggedRegistrations[Tag][Lifetime].Add((registration.Value.Implementation, Services));
                    }
                }
            }

            var fileContentBuilder = new StringBuilder();
            fileContentBuilder.AppendLine("//Auto Generated File");
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine($"namespace {ServiceProviderAttributeGenerator.Namespace}");
            fileContentBuilder.AppendLine("{");
            fileContentBuilder.AppendLine("    public static class ServiceProviderExtensions");
            fileContentBuilder.AppendLine("    {");

            fileContentBuilder.AppendLine("        public static IServiceCollection RegisterDependenciesByAttribute(this IServiceCollection services, params object[] includeTags)");
            fileContentBuilder.AppendLine("        {");
            fileContentBuilder.AppendLine("            return services.AddServicesByAttribute(includeTags);");
            fileContentBuilder.AppendLine("        }");
            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("        public static IServiceCollection AddServicesByAttribute(this IServiceCollection services, params object[] includeTags)");
            fileContentBuilder.AppendLine("        {");

            fileContentBuilder.Append(GetServiceRegistrations(untaggedRegistrations, 12));


            fileContentBuilder.AppendLine("            foreach (string includedTag in includeTags)");
            fileContentBuilder.AppendLine("            {");

            foreach (var taggedRegistration in taggedRegistrations)
            {
                fileContentBuilder.AppendLine($"                if(includedTag == \"{taggedRegistration.Key}\")");
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
        catch(Exception e)
        {
            context.AddSource($"Error.g.cs", e.ToString());
            return;
        }
    }


    private string GetServiceRegistrations(Dictionary<string, List<(string, IEnumerable<string>)>> services, int indent = 0)
    {
        var fileContentBuilder = new StringBuilder();

        foreach (var registration in services)
        {
            foreach (var (Implementation, Services) in registration.Value)
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
                        if (registration.Key == "Transient")
                            fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}<{service}, {Implementation}>();", indent);
                        else
                            fileContentBuilder.AppendLineIndented($"services.Add{registration.Key}(sp => ({service})sp.GetRequiredService<{Services.First()}>());", indent);
                    }
                }
            }

            fileContentBuilder.AppendLine();
        }

        return fileContentBuilder.ToString();
    }

    private IEnumerable<string> GetServiceNames(AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
        => Helpers.GetServices(attributeSyntax, semanticModel, implementation)
            .Where(s => Helpers.IsTypeInHierarchy(s, implementation)).Select(Helpers.GetFullName);

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
