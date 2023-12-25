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
                "Service Type not present on Implementation", 
                "'{0}' does not {1} '{2}'",
                "ServiceRegistration", 
                DiagnosticSeverity.Error, 
                isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor InvalidImplementationType =
            new($"ServiceRegistration002",
                "Invalid object type for service provider",
                "{0}",
                "ServiceRegistration",
                DiagnosticSeverity.Error,
                isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
            ImmutableArray.Create(ServiceNotOnImplementation, InvalidImplementationType);

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
                foreach (var service in Helpers.GetServices(attribute, context.SemanticModel, implementation))
                {
                    if (!Helpers.IsTypeInHierarchy(service, implementation))
                    {
                        context.ReportDiagnostic(
                            Diagnostic.Create(
                                ServiceNotOnImplementation,
                                attribute.GetLocation(),
                                implementation.Locations,
                                implementation.Name,
                                service.TypeKind == TypeKind.Interface ? "implement" : "inherit",
                                service.Name));
                    }
                }

                if (implementation.IsAbstract)
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            InvalidImplementationType,
                            attribute.GetLocation(),
                            implementation.Locations,
                            $"An abstract class cannot use the {attribute.Name} attribute"));
                }

                else if (implementation.IsStatic)
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            InvalidImplementationType,
                            attribute.GetLocation(),
                            implementation.Locations,
                            $"A static class cannot use the {attribute.Name} attribute"));
                }

                else if (!implementation.Constructors.Any(c => c.DeclaredAccessibility == Accessibility.Public))
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            InvalidImplementationType,
                            attribute.GetLocation(),
                            implementation.Locations,
                            $"You need at least one public constructor to use the {attribute.Name} attribute"));
                }
            }
        }
    }
}
