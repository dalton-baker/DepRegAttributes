﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DepRegAttributes;

public abstract class RegisterAttributeBase(params Type[] asTypes) : Attribute
{
    /// <summary>
    /// Used as a filter when registering services
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// Use this to register a Keyed service 
    /// </summary>
    public object? Key { get; set; } = null;

    /// <summary>
    /// The lifetime of the services registered by this attribute
    /// </summary>
    protected abstract ServiceLifetime ServiceLifetime { get; }

    /// <summary>
    /// Registers an implementation type as the configured service types in a service collection.
    /// </summary>
    /// <param name="serviceCollection">The servcie collection</param>
    /// <param name="implementationType">The implementation type being regitstered</param>
    /// <param name="tagFilters">The tags you want to register services for (optional)</param>
    /// <exception cref="CustomAttributeFormatException">Thrown when an implementation type cannot be registered as a provided service type</exception>
    public void RegisterServices(IServiceCollection serviceCollection, Type implementationType, params object[] tagFilters)
    {
        if (Tag is not null && Array.IndexOf(tagFilters, Tag) < 0)
            return;

        var serviceTypes = GetServiceTypes(implementationType);
        var firstType = serviceTypes[0];

        foreach (var type in serviceTypes)
        {
            if (!type.IsAssignableFrom(implementationType))
                throw new CustomAttributeFormatException($"{implementationType.Name} cannot be registered as a {type.Name}.");

            if (type == firstType || ServiceLifetime == ServiceLifetime.Transient)
            {
                serviceCollection.Add(new ServiceDescriptor(type, Key, implementationType, ServiceLifetime));
                continue;
            }

            if(Key is null)
            {
                serviceCollection.Add(new ServiceDescriptor(type, sp => sp.GetRequiredService(firstType), ServiceLifetime));
                continue;
            }

            serviceCollection.Add(new ServiceDescriptor(type, Key, (sp, k) => sp.GetRequiredKeyedService(firstType, k), ServiceLifetime));
        }
    }

    private Type[] GetServiceTypes(Type implementationType)
    {
        Type[] serviceTypes;
        if (asTypes != null && asTypes.Length > 0)
        {
            serviceTypes = asTypes;
        }
        else
        {
            var defaultInterface = implementationType.GetInterface($"I{implementationType.Name}");
            serviceTypes = [defaultInterface ?? implementationType];
        }
        return serviceTypes;
    }
}
