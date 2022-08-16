using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DepRegAttributes
{
    public static class DepRegServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDependanciesByAttribute(
            this IServiceCollection services)
            => services.RegisterDependanciesByAttribute(
                Assembly.GetCallingAssembly());

        public static IServiceCollection RegisterDependanciesByAttribute(
            this IServiceCollection services,
            string filter)
            => services.RegisterDependanciesByAttribute(
                filter,
                Assembly.GetCallingAssembly());

        public static IServiceCollection RegisterDependanciesByAttribute(
            this IServiceCollection services,
            params Assembly[] assemblies)
            => RegisterDependanciesByAttribute(services, null, assemblies);

        public static IServiceCollection RegisterDependanciesByAttribute(
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
