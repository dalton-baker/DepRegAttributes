using Microsoft.CodeAnalysis;
using System;

namespace DepRegAttributes.Analyzer;

public static class INamedSymbolExtensions
{
    public static bool IsTypeInHierarchy(this INamedTypeSymbol implementation, INamedTypeSymbol service)
    {
        foreach (var @interface in implementation.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(@interface, service))
                return true;
        }

        var type = implementation;
        while (type is not null)
        {
            if (SymbolEqualityComparer.Default.Equals(type, service))
                return true;

            type = type.BaseType;
        }

        return false;
    }

    public static bool HasConstructorAccessibility(this INamedTypeSymbol symbol, params Accessibility[] accessibilities)
    {
        foreach (var constructor in symbol.Constructors)
        {
            if (Array.IndexOf(accessibilities, constructor.DeclaredAccessibility) >= 0)
                return true;
        }
        return false;
    }
}
