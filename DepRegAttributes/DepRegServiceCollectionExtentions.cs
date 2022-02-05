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
        {
            return services.RegisterDependanciesByAttribute(
                Assembly.GetCallingAssembly());
        }

        public static IServiceCollection RegisterDependanciesByAttribute(
            this IServiceCollection services,
            Assembly assembly)
        {
            foreach(Type type in assembly.GetTypes())
            {
                var regAttributes = type
                    .GetCustomAttributes(typeof(RegistrationAttributeBase), false)
                    .Select(a => a as RegistrationAttributeBase)
                    .ToList();

                regAttributes.ForEach(reg => reg.RegisterServcices(services, type));
            }

            return services;
        }
    }
}
