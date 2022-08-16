using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DepRegAttributes
{
    public static class DepRegServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDependenciesByAttribute(
            this IServiceCollection services)
            => services.RegisterDependenciesByAttribute(
                Assembly.GetCallingAssembly());

        public static IServiceCollection RegisterDependenciesByAttribute(
            this IServiceCollection services,
            string filter)
            => services.RegisterDependenciesByAttribute(
                filter,
                Assembly.GetCallingAssembly());

        public static IServiceCollection RegisterDependenciesByAttribute(
            this IServiceCollection services,
            params Assembly[] assemblies)
            => RegisterDependenciesByAttribute(services, null, assemblies);

        public static IServiceCollection RegisterDependenciesByAttribute(
            this IServiceCollection services,
            string filter,
            params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    var regAttributes = type
                        .GetCustomAttributes(typeof(RegistrationAttributeBase), false)
                        .Select(a => a as RegistrationAttributeBase);

                    foreach (var regAttribute in regAttributes)
                    {
                        regAttribute.RegisterServices(services, type, filter);
                    }
                }
            }

            return services;
        }
    }
}
