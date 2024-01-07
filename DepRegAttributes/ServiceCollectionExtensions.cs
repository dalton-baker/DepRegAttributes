using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DepRegAttributes;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register services by thier attributes for your current Assembly
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="tagFilters">The tags you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid of rthe implementation type</exception>
    /// <returns>The service collection, used to chain fluent calls</returns>
    [Obsolete("Use 'AddByAttribute(filters)' instead")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        params object[] tagFilters)
        => services.AddByAttribute(tagFilters);

    /// <summary>
    /// Register services by thier attributes for a specific assembly
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="assembly">The Assembly you are registering services from</param>
    /// <param name="tagFilters">The tags you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid of rthe implementation type</exception>
    /// <returns>The service collection, used to chain fluent calls</returns>
    [Obsolete("Use 'AddByAttribute(assembly, filters)' instead")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        params object[] tagFilters)
        => services.AddByAttribute(assembly, tagFilters);

    /// <summary>
    /// Register services by thier attributes for your current Assembly
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="tagFilters">The tags you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid of rthe implementation type</exception>
    /// <returns>The service collection, used to chain fluent calls</returns>
    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        params object[] tagFilters)
        => services.AddByAttribute(
            Assembly.GetCallingAssembly(),
            tagFilters);

    /// <summary>
    /// Register services by thier attributes for a specific assembly
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="assembly">The Assembly you are registering services from</param>
    /// <param name="tagFilters">The tags you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid of rthe implementation type</exception>
    /// <returns>The service collection, used to chain fluent calls</returns>
    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        params object[] tagFilters)
    {
        foreach (Type type in assembly.GetTypes())
        {
            foreach (var attribute in type.GetCustomAttributes(typeof(RegisterAttributeBase), false))
            {
                if(attribute is RegisterAttributeBase registerAttributeBase)
                {
                    registerAttributeBase.RegisterServices(services, type, tagFilters);
                }
            }
        }
        return services;
    }
}
