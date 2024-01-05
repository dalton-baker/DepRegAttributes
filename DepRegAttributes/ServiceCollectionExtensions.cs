using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Linq;

namespace DepRegAttributes;

public static class ServiceCollectionExtensions
{
    [Obsolete("Use 'AddByAttribute(filters)' instead")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        params object[] filters)
        => services.AddByAttribute(filters);

    [Obsolete("Use 'AddByAttribute(assembly, filters)' instead")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        params object[] filters)
        => services.AddByAttribute(assembly, filters);

    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        params object[] filters)
        => services.AddByAttribute(
            Assembly.GetCallingAssembly(),
            filters);

    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        params object[] filters)
    {
        foreach (Type type in assembly.GetTypes())
        {
            var regAttributes = type
                .GetCustomAttributes(typeof(RegisterAttributeBase), false)
                .Select(a => a as RegisterAttributeBase);

            foreach (var regAttribute in regAttributes)
                regAttribute.RegisterServices(services, type, filters);
        }
        return services;
    }
}
