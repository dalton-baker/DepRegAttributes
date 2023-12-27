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

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
            ImmutableArray.Create(ServiceNotOnImplementation, InvalidImplementationType, InvalidAutoIncludedService);

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
                    else if (service.DeclaredAccessibility != Accessibility.Public)
                    {
                        if (service.Name == $"I{implementation.Name}" && location == attribute.GetLocation())
                        {
                            context.ReportDiagnostic(
                                Diagnostic.Create(
                                    InvalidAutoIncludedService,
                                    location,
                                    $"{service.Name} is not public, so it will not be automatically included for {implementation.Name}"));
                        }
                        else
                        {
                            context.ReportDiagnostic(
                                Diagnostic.Create(
                                    ServiceNotOnImplementation,
                                    location,
                                    $"{service.Name} is private, you cannot use it as a service type"));
                        }
                    }
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
                else if (!implementation.Constructors.Any(c => c.DeclaredAccessibility == Accessibility.Public))
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            InvalidImplementationType,
                            implementation.Locations[0],
                            $"You need at least one public constructor to use the {attribute.Name} attribute"));
                }
                else if (implementation.DeclaredAccessibility != Accessibility.Public)
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
}
