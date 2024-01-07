using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class RegisterAttributeBase(ServiceLifetime serviceLifetime, params Type[] serviceTypes) : Attribute
{
    /// <summary>
    /// Used as a filter when registering services
    /// </summary>
    public object? Tag { get; set; }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Use this to register a Keyed service 
    /// </summary>
    public object? Key { get; set; } = null;
#endif

    /// <summary>
    /// The lifetime of the services registered by this attribute
    /// </summary>
    internal ServiceLifetime ServiceLifetime => serviceLifetime;

    /// <summary>
    /// The Service types passed to the attribute
    /// </summary>
    internal Type[] ServiceTypes => serviceTypes;
}
