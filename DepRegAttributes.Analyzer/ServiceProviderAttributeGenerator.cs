using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Text;

namespace DepRegAttributes.Analyzer
{
    [Generator]
    public class ServiceProviderAttributeGenerator : ISourceGenerator
    {
        private const int _maxGenericArgs = 10;

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.Compilation is not CSharpCompilation csharpCompilation)
                return;

            var @namespace = csharpCompilation.GetLibraryNamespace();

            var diReferenceVersion = new Version(0, 0, 0, 0);
            if (csharpCompilation.ReferencedAssemblyNames is not null)
            {
                foreach (var assembly in csharpCompilation.ReferencedAssemblyNames)
                {
                    if (assembly.Name == "Microsoft.Extensions.DependencyInjection")
                    {
                        diReferenceVersion = assembly.Version;
                        break;
                    }
                }
            }

            foreach (var attribute in Consts.AttributeList)
            {
                context.AddSource($"{attribute}.g.cs", GetBaseAttributeFileContents(attribute, @namespace, diReferenceVersion >= new Version(8, 0, 0, 0)));
                if(csharpCompilation.LanguageVersion >= LanguageVersion.CSharp11)
                    context.AddSource($"{attribute}Generics.g.cs", GetGenericAttributeFileContents(attribute, @namespace));
            }

            if (csharpCompilation.LanguageVersion >= LanguageVersion.CSharp10)
            {
                context.AddSource($"RegisterAttributeGlobalUsing.g.cs",
                    $"//Auto Generated File\n" +
                    $"global using {@namespace};");
            }
        }

        private string GetBaseAttributeFileContents(string attributeName, string @namespace, bool includeKey)
        {
            var builder = new StringBuilder();
            builder.AppendLine("//Auto Generated File");
            builder.AppendLine();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine($"namespace {@namespace}");
            builder.AppendLine("{");
            builder.AppendLine("    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]");
            builder.AppendLine($"    public class {attributeName}: Attribute");
            builder.AppendLine("    {");
            builder.AppendLine("        public object Tag { get; set; }");
            builder.AppendLine();
            if (includeKey)
            {
                builder.AppendLine("        public object Key { get; set; }");
                builder.AppendLine();
            }
            builder.AppendLine($"        public {attributeName}(params Type[] types)");
            builder.AppendLine("        {");
            builder.AppendLine("            Type[] typesInternal = types;");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            builder.AppendLine();
            return builder.ToString();
        }

        private string GetGenericAttributeFileContents(string attributeName, string @namespace)
        {
            var builder = new StringBuilder();
            builder.AppendLine("//Auto Generated File");
            builder.AppendLine();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine($"namespace {@namespace}");
            builder.AppendLine("{");

            for (int i = 1; i <= _maxGenericArgs; i++)
            {
                var allTs = new StringBuilder();
                for (int t = 1; t < i; t++)
                {
                    allTs.Append("T").Append(t).Append(", ");
                }
                allTs.Append("T").Append(i);

                builder.AppendLine();
                builder.AppendLine("    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]");
                builder.AppendLine($"    public class {attributeName}<{allTs}> : {attributeName}");
                builder.AppendLine("    {");
                builder.AppendLine("    }");
            }

            builder.AppendLine("}");
            builder.AppendLine();
            return builder.ToString();
        }


    }
}
