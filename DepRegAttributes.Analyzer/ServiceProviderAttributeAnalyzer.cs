using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DepRegAttributes.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ServiceProviderAttributeAnalyzer : DiagnosticAnalyzer
{
    #region Diagnostic Descriptors
    private static readonly DiagnosticDescriptor ServiceNotOnImplementation =
        new($"ServiceRegistration001",
            "Invalid service type",
            "{0}",
            "ServiceRegistration",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor InvalidImplementationType =
        new($"ServiceRegistration002",
            "Invalid implementation type",
            "{0}",
            "ServiceRegistration",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor PotentialBadParameter =
        new($"ServiceRegistration003",
            "Potential bad parameter",
            "{0}",
            "ServiceRegistration",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(ServiceNotOnImplementation, InvalidImplementationType, PotentialBadParameter);
    #endregion

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeAttributeUsage, SyntaxKind.ClassDeclaration);
    }

    private void AnalyzeAttributeUsage(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classDeclaration)
            return;

        var implementation = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
        if (implementation is null)
            return;

        var hasAttribute = false;
        foreach (var attributeList in classDeclaration.AttributeLists)
        {
            foreach (var attribute in attributeList.Attributes)
            {
                if(attribute.IsRegistrationAttribute(context.SemanticModel))
                {
                    hasAttribute = true;
                    AnalyzeServiceTypes(context, implementation, attribute);
                    AnalyzeProperties(context, attribute);
                }
            }
        }

        if(hasAttribute)
            AnalyzeImplementation(context, implementation);
    }

    private void AnalyzeServiceTypes(
        SyntaxNodeAnalysisContext context,
        INamedTypeSymbol implementation,
        AttributeSyntax attribute)
    {
        foreach (var (service, location) in attribute.GetServicesWithLocations(context.SemanticModel, implementation))
        {
            if (!service.IsUnboundGenericType && !implementation.IsTypeInHierarchy(service))
            {
                var message = service.TypeKind == TypeKind.Interface
                    ? $"'{implementation.Name}' does not implement '{service.Name}'"
                    : $"'{implementation.Name}' does not inherit '{service.Name}'";

                context.ReportDiagnostic(Diagnostic.Create(
                    ServiceNotOnImplementation,
                    location,
                    message));
            }
        }
    }

    private void AnalyzeProperties(SyntaxNodeAnalysisContext context, AttributeSyntax attribute)
    {
        if (attribute.ArgumentList is null)
            return;

        foreach (var argument in attribute.ArgumentList.Arguments)
        {
            if (argument.NameEquals is null || argument.NameEquals.Name.Identifier.Text is not (Const.TagProperty or Const.KeyProperty))
                continue;

            if (argument.Expression is ImplicitArrayCreationExpressionSyntax)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    PotentialBadParameter,
                    argument.Expression.GetLocation(),
                    $"Using an array as a {argument.NameEquals.Name.Identifier.Text} will result in a reference comparison, you will not be able to resolve it."));
            }
        }
    }

    private void AnalyzeImplementation(
        SyntaxNodeAnalysisContext context,
        INamedTypeSymbol implementation)
    {
        if (implementation.IsAbstract)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                InvalidImplementationType,
                implementation.Locations[0],
                $"An abstract class cannot use a Register Attribute"));
        }
        else if (implementation.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                InvalidImplementationType,
                implementation.Locations[0],
                $"A static class cannot use a Register Attribute"));
        }
        else if (!implementation.HasConstructorAccessibility(Accessibility.Public, Accessibility.Internal))
        {
            context.ReportDiagnostic(Diagnostic.Create(
                InvalidImplementationType,
                implementation.Locations[0],
                $"At least one public or internal constructor is required to use a Register Attribute"));
        }
    }
}
