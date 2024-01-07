using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace DepRegAttributes.Analyzer;

public static class AttributeSyntaxExtensions
{
    public static IEnumerable<(INamedTypeSymbol Service, Location Location)> GetServicesWithLocations(
        this AttributeSyntax attributeSyntax, SemanticModel semanticModel, INamedTypeSymbol implementation)
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
                else if (argument is PredefinedTypeSyntax predefinedTypeSyntax)
                {
                    var symbol = semanticModel.GetSymbolInfo(predefinedTypeSyntax).Symbol;
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
                    var symbol = semanticModel.GetSymbolInfo(typeExpression.Type).Symbol;
                    if (symbol is INamedTypeSymbol namedTypeSymbol)
                        services.Add((namedTypeSymbol, typeExpression.Type.GetLocation()));
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

    public static bool IsRegistrationAttribute(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        var attribute = semanticModel.GetSymbolInfo(attributeSyntax).Symbol;
        if (attribute is null || attribute.ContainingAssembly is null || attribute.ContainingAssembly.Name is not Const.LibraryNamespace)
            return false;

        var name = attribute.ToDisplayString(
            new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces));

        return name is Const.RegisterTransientAttribute or Const.RegisterSingletonAttribute or Const.RegisterScopedAttribute;
    }
}
