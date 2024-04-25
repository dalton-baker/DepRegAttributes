using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DepRegAttributes;

/// <summary>
/// Holds all of the IServiceCollection extentions for adding services with Register attributes.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register services by attribute for your current Assembly.
    /// </summary>
    /// <param name="services">The Service Collection</param>
    /// <param name="tagFilter">The tag you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid for the implementation type.</exception>
    /// <returns>A reference to this instance after the opperation has completed.</returns>
    [Obsolete("Use AddByAttribute()")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        object? tagFilter = null)
        => services.AddByAttribute(Assembly.GetCallingAssembly(), tagFilter);

    /// <summary>
    /// Register services by attribute for a specific Assembly.
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="assembly">The Assembly you are registering services from</param>
    /// <param name="tagFilter">The tag you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid for the implementation type.</exception>
    /// <returns>A reference to this instance after the opperation has completed.</returns>
    [Obsolete("Use AddByAttribute()")]
    public static IServiceCollection RegisterDependenciesByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        object? tagFilter = null)
        => services.AddByAttribute(assembly, tagFilter);

    /// <summary>
    /// Register services by attribute for your current Assembly.
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="tagFilter">The tag you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid for the implementation type.</exception>
    /// <returns>A reference to this instance after the opperation has completed.</returns>
    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        object? tagFilter = null)
        => services.AddByAttribute(
            Assembly.GetCallingAssembly(),
            tagFilter);

    /// <summary>
    /// Register services by attribute for a specific Assembly.
    /// </summary>
    /// <param name="services">The service Collection</param>
    /// <param name="assembly">The Assembly you are registering services from</param>
    /// <param name="tagFilter">The tag you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an attribute has a service type that is not valid for the implementation type.</exception>
    /// <returns>A reference to this instance after the opperation has completed.</returns>
    public static IServiceCollection AddByAttribute(
        this IServiceCollection services,
        Assembly assembly,
        object? tagFilter = null)
    {
        foreach (Type implementationType in assembly.GetTypes())
        {
            foreach (var attribute in implementationType.GetCustomAttributes(typeof(RegisterAttributeBase), false))
            {
                if(attribute is RegisterAttributeBase registerAttribute)
                {
                    if (!EqualityComparer<object?>.Default.Equals(tagFilter, registerAttribute.Tag))
                        continue;

                    services.AddServiceForAttribute(implementationType, registerAttribute);
                }
            }
        }
        return services;
    }

    private static IServiceCollection AddServiceForAttribute(
        this IServiceCollection services, 
        Type implementationType,
        RegisterAttributeBase registerAttribute)
    {
        var serviceTypes = registerAttribute.ServiceTypes.Length > 0 
            ? registerAttribute.ServiceTypes 
            : [implementationType.GetDefaultServiceType()];
        var firstType = serviceTypes[0];

        foreach (var type in serviceTypes)
        {
            if (!implementationType.IsGenericType && !type.IsAssignableFrom(implementationType))
                throw new CustomAttributeFormatException($"{implementationType.Name} cannot be registered as a {type.Name}.");

            if (type.Equals(firstType) || registerAttribute.ServiceLifetime is ServiceLifetime.Transient)
                services.Add(new ServiceDescriptor(type, implementationType, registerAttribute.ServiceLifetime));
            else
                services.Add(new ServiceDescriptor(type, sp => sp.GetRequiredService(firstType), registerAttribute.ServiceLifetime));
        }

        return services;
    }

    private static Type GetDefaultServiceType(this Type implementationType)
    {
        if (implementationType.IsGenericType)
            return implementationType;
        return implementationType.GetInterface($"I{implementationType.Name}") ?? implementationType;
    }
}
