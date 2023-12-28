using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DepRegAttributes.Analyzer
{
    public static class Helpers
    {
        public static IEnumerable<INamedTypeSymbol> GetServices(AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
            => GetServicesWithLocations(attributeSyntax, semanticModel, implementation).Select(s => s.Service);

        public static IEnumerable<(INamedTypeSymbol Service, Location Location)> GetServicesWithLocations(
            AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
        {
            var services = new List<(INamedTypeSymbol, Location)>();

            //Generic arguments
            if (attributeSyntax.Name is GenericNameSyntax genericNameSyntax &&
                genericNameSyntax.TypeArgumentList is not null)
            {
                foreach (var argument in genericNameSyntax.TypeArgumentList.Arguments)
                {
                    if (argument is SimpleNameSyntax simpleName)
                    {
                        var symbol = semanticModel.GetSymbolInfo(simpleName).Symbol;
                        if (symbol is INamedTypeSymbol namedTypeSymbol)
                            services.Add((namedTypeSymbol, argument.GetLocation()));
                    }
                }
            }

            //Constructor arguments
            if (attributeSyntax.ArgumentList is not null)
            {
                foreach (var argument in attributeSyntax.ArgumentList.Arguments)
                {
                    if (argument.NameEquals?.Name is not null)
                        continue;

                    else if (argument.Expression is TypeOfExpressionSyntax typeExpression)
                    {
                        var symbol = semanticModel.GetTypeInfo(typeExpression.Type).Type ??
                            semanticModel.GetSymbolInfo(typeExpression.Type).Symbol;
                        if (symbol is INamedTypeSymbol namedTypeSymbol)
                            services.Add((namedTypeSymbol, argument.GetLocation()));
                    }
                }
            }

            if (services.Count == 0 && implementation is INamedTypeSymbol namedSymbol)
            {
                foreach (var interfaceSymbol in namedSymbol.Interfaces)
                {
                    if (interfaceSymbol.Name == $"I{implementation.Name}")
                    {
                        services.Add((interfaceSymbol, attributeSyntax.GetLocation()));
                        break;
                    }
                }
            }

            return services;
        }

        public static bool IsTypeInHierarchy(INamedTypeSymbol service, INamedTypeSymbol implementation)
        {
            if (implementation.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, service)))
                return true;

            var type = implementation;
            while (type is not null)
            {
                if (SymbolEqualityComparer.Default.Equals(type, service))
                    return true;

                type = type.BaseType;
            }

            return false;
        }

        public static string GetAttributeName(AttributeSyntax attributeSyntax)
        {
            if (attributeSyntax.Name is GenericNameSyntax genericNameSyntax)
            {
                return genericNameSyntax.Identifier.Text;
            }
            return attributeSyntax.Name.ToString();
        }

        public static bool IsRegistrationAttribute(AttributeSyntax attributeSyntax, SemanticModel semanticModel)
        {
            //TODO: Look at the namespace of the attribute to ensure it's the correct one
            return GetAttributeName(attributeSyntax) is "RegisterTransient" or "RegisterSingleton" or "RegisterScoped" or
                "RegisterTransientAttribute" or "RegisterSingletonAttribute" or "RegisterScopedAttribute";
        }

        public static string GetFullName(ISymbol symbol)
        {
            var (@namespace, type) = GetNamespaceAndName(symbol);
            return string.IsNullOrEmpty(@namespace) ? type : $"{@namespace}.{type}";
        }

        public static (string Namespace, string TypeName) GetNamespaceAndName(ISymbol symbol)
        {
            var nestedTypes = new List<string>();
            var type = symbol;
            while (type is not null)
            {
                nestedTypes.Insert(0, type.Name);
                type = type.ContainingType;
            }
            var typeName = string.Join(".", nestedTypes);

            if (symbol.ContainingNamespace is null || symbol.ContainingNamespace.IsGlobalNamespace)
                return (string.Empty, typeName);

            var namespaces = new List<string>();
            var currentNamespace = symbol.ContainingNamespace;

            while (!currentNamespace.IsGlobalNamespace)
            {
                namespaces.Insert(0, currentNamespace.Name);
                currentNamespace = currentNamespace.ContainingNamespace;
            }

            return (string.Join(".", namespaces), typeName);
        }

        public static (string Namespace, string Name) GetPropertyArgument(AttributeSyntax attributeSyntax, SemanticModel semanticModel, string tag)
        {
            if (attributeSyntax.ArgumentList is null)
                return (string.Empty, string.Empty);


            foreach (var argument in attributeSyntax.ArgumentList.Arguments)
            {
                if (argument.NameEquals is null || argument.NameEquals.Name.Identifier.Text != tag)
                    continue;

                if (argument.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    var symbol = semanticModel.GetSymbolInfo(memberAccess.Expression).Symbol;
                    if (symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        var (@namespace, name) = GetNamespaceAndName(namedTypeSymbol);
                        return (@namespace, $"{name}.{memberAccess.Name}");
                    }
                }

                else if (argument.Expression is IdentifierNameSyntax identifierName)
                {
                    var symbol = semanticModel.GetSymbolInfo(identifierName).Symbol;
                    if (symbol is IFieldSymbol fieldSymbol)
                    {
                        if(fieldSymbol.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
                        {
                            var (@namespace, name) = GetNamespaceAndName(fieldSymbol.ContainingType);
                            return (@namespace, $"{name}.{fieldSymbol.Name}");
                        }

                        if(fieldSymbol.ConstantValue is not null)
                        {
                            if (fieldSymbol.ConstantValue is string value)
                            {
                                return (string.Empty, $"\"{value}\"");
                            }
                            return (string.Empty, fieldSymbol.ConstantValue.ToString());
                        }

                        return (string.Empty, string.Empty);
                    }
                }

                return (string.Empty, argument.Expression.ToFullString());
            }

            return (string.Empty, string.Empty);
        }

        public static string GetLibraryNamespace(this Compilation complation)
            => string.IsNullOrEmpty(complation.AssemblyName) ? Consts.LibraryNamespace : $"{complation.AssemblyName}.{Consts.LibraryNamespace}";
    }


    public static class Consts
    {
        public const string LibraryNamespace = "DepRegAttributes";

        public const string Transient = nameof(Transient);
        public const string Singleton = nameof(Singleton);
        public const string Scoped = nameof(Scoped);

        public const string TansientAttribute = $"Register{Transient}Attribute";
        public const string SingletonAttribute = $"Register{Singleton}Attribute";
        public const string ScopedAttribute = $"Register{Scoped}Attribute";

        public static List<string> AttributeList => new() { TansientAttribute, SingletonAttribute, ScopedAttribute };
    }
}
