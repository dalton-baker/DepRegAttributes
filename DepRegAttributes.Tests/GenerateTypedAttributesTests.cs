using System.Text;

namespace DepRegAttributes.Tests;

[TestClass]
[Ignore("Don't run unless you want to generate more attributes")]
public class GenerateTypedAttributesTests : UnitTestBase
{
    [TestMethod]
    public void GenerateRegisterTransientAttributes()
    {
        var fileContents = GetFileContents<RegisterTransientAttribute>(10);
        TestContext.WriteLine(fileContents);
    }


    [TestMethod]
    public void GenerateRegisterSingletonAttributes()
    {
        var fileContents = GetFileContents<RegisterSingletonAttribute>(10);
        TestContext.WriteLine(fileContents);
    }


    [TestMethod]
    public void GenerateRegisterScopedAttributes()
    {
        var fileContents = GetFileContents<RegisterScopedAttribute>(10);
        TestContext.WriteLine(fileContents);
    }

    private string GetFileContents<T>(int max) where T : RegistrationAttributeBase
    {
        var attributeName = typeof(T).Name;
        var builder = new StringBuilder();

        builder.AppendLine("using System;");
        builder.AppendLine();
        builder.AppendLine("#if NET7_0");
        builder.AppendLine("namespace DepRegAttributes;");

        for (int i = 1; i <= max; i++)
        {
            var allTs = string.Join(", ", Enumerable.Range(1, i).Select(x => $"T{x}"));
            var allTypes = string.Join(", ", Enumerable.Range(1, i).Select(x => $"typeof(T{x})"));

            builder.AppendLine();
            if (typeof(T) == typeof(RegisterTransientAttribute))
            {
                builder.AppendLine("[AttributeUsage(AttributeTargets.Class)]");
            }
            else
            {
                builder.AppendLine("[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]");
            }

            builder.AppendLine($"public class {attributeName}<{allTs}> : {attributeName}");
            builder.AppendLine("{");
            builder.AppendLine("    private static readonly Type[] _types =");
            builder.AppendLine($"        new[] {{ {allTypes} }};");
            builder.AppendLine();
            builder.AppendLine($"    public {attributeName}()");
            builder.AppendLine("         : base(_types)");
            builder.AppendLine("    {");
            builder.AppendLine("    }");
            builder.AppendLine();
            builder.AppendLine($"    public {attributeName}(params string[] filters)");
            builder.AppendLine("         : base(filters, _types)");
            builder.AppendLine("    {");
            builder.AppendLine("    }");
            builder.AppendLine("}");
        }
        builder.AppendLine("#endif");

        File.WriteAllText($"../../../../DepRegAttributes/{attributeName}Generics.cs", builder.ToString());

        return builder.ToString();
    }
}