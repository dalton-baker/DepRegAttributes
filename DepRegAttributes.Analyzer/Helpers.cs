using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Collections.Generic;

namespace DepRegAttributes.Analyzer
{
    public static class Helpers
    {
        public static IEnumerable<INamedTypeSymbol> GetServices(AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
        {
            var services = new List<INamedTypeSymbol>();

            if (attributeSyntax.Name is GenericNameSyntax genericNameSyntax && genericNameSyntax.TypeArgumentList is not null)
            {
                services.AddRange(genericNameSyntax.TypeArgumentList.Arguments
                    .OfType<SimpleNameSyntax>()
                    .Select(a => semanticModel.GetSymbolInfo(a).Symbol)
                    .OfType<INamedTypeSymbol>());
            }

            if (attributeSyntax.ArgumentList is not null)
            {
                services.AddRange(attributeSyntax.ArgumentList.Arguments
                    .Select(a => a.Expression)
                    .OfType<TypeOfExpressionSyntax>()
                    .Select(a => semanticModel.GetTypeInfo(a.Type).Type ?? semanticModel.GetSymbolInfo(a.Type).Symbol)
                    .OfType<INamedTypeSymbol>());
            }

            if (!services.Any() && implementation is INamedTypeSymbol namedSymbol)
            {
                var impliedInterface = namedSymbol.Interfaces.FirstOrDefault(i => i.Name == $"I{implementation.Name}");
                if (impliedInterface is not null)
                {
                    services.Add(impliedInterface);
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
            if (symbol.ContainingNamespace is null || symbol.ContainingNamespace.IsGlobalNamespace)
                return symbol.Name;

            var namespaces = new List<string>();
            var currentNamespace = symbol.ContainingNamespace;

            while (!currentNamespace.IsGlobalNamespace)
            {
                namespaces.Insert(0, currentNamespace.Name);
                currentNamespace = currentNamespace.ContainingNamespace;
            }

            return $"{string.Join(".", namespaces)}.{symbol.Name}";
        }

        public static string GetPropertyArgument(AttributeSyntax attributeSyntax, SemanticModel semanticModel, string tag)
        {
            if (attributeSyntax.ArgumentList is null)
                return string.Empty;


            foreach (var argument in attributeSyntax.ArgumentList.Arguments)
            {
                if (argument.NameEquals is null || argument.NameEquals.Name.Identifier.Text != tag)
                    continue;

                if (argument.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    var symbol = semanticModel.GetSymbolInfo(memberAccess.Expression).Symbol;
                    if (symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        // Access the namespace and name
                        return $"{GetFullName(namedTypeSymbol)}.{memberAccess.Name}";
                    }
                }

                else if (argument.Expression is IdentifierNameSyntax identifierName)
                {
                    var symbol = semanticModel.GetSymbolInfo(identifierName).Symbol;
                    if (symbol is IFieldSymbol fieldSymbol)
                    {
                        if(fieldSymbol.DeclaredAccessibility == Accessibility.Public)
                        {
                            return $"{GetFullName(fieldSymbol.ContainingType)}.{fieldSymbol.Name}";
                        }

                        if(fieldSymbol.ConstantValue is not null)
                        {
                            if (fieldSymbol.ConstantValue is string value)
                            {
                                return $"\"{value}\"";
                            }
                            return fieldSymbol.ConstantValue.ToString();
                        }

                        return string.Empty;
                    }
                }

                return argument.Expression.ToFullString();
            }

            return string.Empty;
        }

        public static List<string> AttributeList => ["RegisterTransientAttribute", "RegisterScopedAttribute", "RegisterSingletonAttribute"];
    }
}
