using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes;

/// <summary>
/// The base calss for Register Attributes.
/// </summary>
/// <param name="serviceLifetime">The lifetime of the service registered with the attribute</param>
/// <param name="serviceTypes">The service types for the class registered with the attribute</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class RegisterAttributeBase(ServiceLifetime serviceLifetime, params Type[] serviceTypes) : Attribute
{
    /// <summary>
    /// Used as a filter when registering services
    /// </summary>
    public object? Tag { get; set; } = null;
    
    /// <summary>
    /// The lifetime of the services registered by this attribute
    /// </summary>
    internal ServiceLifetime ServiceLifetime => serviceLifetime;

    /// <summary>
    /// The Service types passed to the attribute
    /// </summary>
    internal Type[] ServiceTypes => serviceTypes;
}
