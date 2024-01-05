using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepRegAttributes.Analyzer
{
    public static class Helpers
    {
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
            return GetAttributeName(attributeSyntax) is 
                Consts.TansientShortAttribute or Consts.SingletonShortAttribute or Consts.ScopedShortAttribute or
                Consts.TansientAttribute or Consts.SingletonAttribute or Consts.ScopedAttribute;
        }

        public static HashSet<string> GetNamespaces(this INamedTypeSymbol symbol)
        {
            var namespaces = new List<string>
            {
                symbol.ContainingNamespace is null ? string.Empty : symbol.ContainingNamespace.ToDisplayString()
            };

            if (symbol.IsGenericType)
            {
                foreach (var arg in symbol.TypeArguments)
                {
                    namespaces.Add(arg.ContainingNamespace is null ? string.Empty : arg.ContainingNamespace.ToDisplayString());
                    if(arg is INamedTypeSymbol namedArg)
                    {
                        namespaces.AddRange(GetNamespaces(namedArg));
                    }
                }
            }

            namespaces.RemoveAll(string.IsNullOrEmpty);
            return new HashSet<string>(namespaces);
        }
    }

    public static class Consts
    {
        public const string LibraryNamespace = "DepRegAttributes";

        public const string Transient = nameof(Transient);
        public const string Singleton = nameof(Singleton);
        public const string Scoped = nameof(Scoped);

        public const string TansientShortAttribute = $"Register{Transient}";
        public const string SingletonShortAttribute = $"Register{Singleton}";
        public const string ScopedShortAttribute = $"Register{Scoped}";

        public const string TansientAttribute = $"{TansientShortAttribute}Attribute";
        public const string SingletonAttribute = $"{SingletonShortAttribute}Attribute";
        public const string ScopedAttribute = $"{ScopedShortAttribute}Attribute";

        public static List<string> AttributeList => new() { TansientAttribute, SingletonAttribute, ScopedAttribute };
    }
}
