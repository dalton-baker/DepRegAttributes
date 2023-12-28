using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DepRegAttributes.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ServiceProviderAttributeAnalyzer : DiagnosticAnalyzer
    {
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

        private static readonly DiagnosticDescriptor InvalidAutoIncludedService =
            new($"ServiceRegistration003",
                "Invalid auto-included service",
                "{0}",
                "ServiceRegistration",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor PotentialBadParameter =
            new($"ServiceRegistration004",
                "Potential bad parameter",
                "{0}",
                "ServiceRegistration",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
            ImmutableArray.Create(
                ServiceNotOnImplementation, 
                InvalidImplementationType, 
                InvalidAutoIncludedService,
                PotentialBadParameter);

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

            if(implementation is null)
                return;

            var attributes = classDeclaration.AttributeLists
                .SelectMany(a => a.Attributes)
                .Where(a => Helpers.IsRegistrationAttribute(a, context.SemanticModel));

            foreach(var attribute in attributes)
            {
                foreach (var (service, location) in Helpers.GetServicesWithLocations(attribute, context.SemanticModel, implementation))
                {
                    if (!Helpers.IsTypeInHierarchy(service, implementation))
                    {
                        var message = service.TypeKind == TypeKind.Interface
                            ? $"'{implementation.Name}' does not implement '{service.Name}'"
                            : $"'{implementation.Name}' does not inherit '{service.Name}'";

                        context.ReportDiagnostic(
                            Diagnostic.Create(
                                ServiceNotOnImplementation,
                                location,
                                message));
                    }
                    else if (service.DeclaredAccessibility is not (Accessibility.Public or Accessibility.Internal))
                    {
                        if (service.Name == $"I{implementation.Name}" && location == attribute.GetLocation())
                        {
                            context.ReportDiagnostic(
                                Diagnostic.Create(
                                    InvalidAutoIncludedService,
                                    location,
                                    $"{service.Name} is not public or internal, so it will not be automatically included for {implementation.Name}"));
                        }
                        else
                        {
                            context.ReportDiagnostic(
                                Diagnostic.Create(
                                    ServiceNotOnImplementation,
                                    location,
                                    $"{service.Name} is not public or internal, you cannot use it as a service type"));
                        }
                    }
                }


                AnalyzeImplementation(context, implementation, attribute);
                AnalyzeProperties(context, attribute);
            }
        }

        private void AnalyzeProperties(SyntaxNodeAnalysisContext context, AttributeSyntax attribute)
        {
            if (attribute.ArgumentList is null)
                return;

            foreach (var argument in attribute.ArgumentList.Arguments)
            {
                if (argument.NameEquals is null || argument.NameEquals.Name.Identifier.Text is not ("Tag" or "Key"))
                    continue;

                if(argument.Expression is ImplicitArrayCreationExpressionSyntax)
                {
                    context.ReportDiagnostic(
                    Diagnostic.Create(
                        PotentialBadParameter,
                        argument.GetLocation(),
                        $"Using an array as a {argument.NameEquals.Name.Identifier.Text} will result in a reference comparison, you will not be able to resolve it."));
                }
            }
        }

        private void AnalyzeImplementation(
            SyntaxNodeAnalysisContext context, 
            INamedTypeSymbol implementation, 
            AttributeSyntax attribute)
        {
            if (implementation.IsUnboundGenericType)
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        InvalidImplementationType,
                        implementation.Locations[0],
                        $"An unbound generic cannot use the {attribute.Name} attribute"));
            }

            if (implementation.IsAbstract)
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        InvalidImplementationType,
                        implementation.Locations[0],
                        $"An abstract class cannot use the {attribute.Name} attribute"));
            }
            else if (implementation.IsStatic)
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        InvalidImplementationType,
                        implementation.Locations[0],
                        $"A static class cannot use the {attribute.Name} attribute"));
            }
            else if (!implementation.Constructors.Any(c => c.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal))
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        InvalidImplementationType,
                        implementation.Locations[0],
                        $"You need at least one public constructor to use the {attribute.Name} attribute"));
            }
            else if (implementation.DeclaredAccessibility is not (Accessibility.Public or Accessibility.Internal))
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        InvalidImplementationType,
                        implementation.Locations[0],
                        $"Only public classes can use the {attribute.Name} attribute"));
            }
        }
    }
}
