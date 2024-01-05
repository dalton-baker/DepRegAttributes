using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DepRegAttributes;

public abstract class RegisterAttributeBase(params Type[] asTypes) : Attribute
{
    private readonly Type[] _asTypes = asTypes;

    public object Tag { get; set; }

#if NET8_0_OR_GREATER
    public object Key { get; set; }
#endif

    protected abstract ServiceLifetime ServiceLifetime { get; }

    public void RegisterServices(IServiceCollection serviceCollection, Type implementationType, params object[] filters)
    {
        if (Tag != null && !filters.Contains(Tag))
            return;

        var asTypes = _asTypes == null || _asTypes.Length == 0
            ? [ implementationType.GetInterface($"I{implementationType.Name}") ?? implementationType ]
            : _asTypes;

        foreach (var type in asTypes)
        {
            if (!type.IsAssignableFrom(implementationType))
                throw new CustomAttributeFormatException($"{implementationType.Name} cannot be registered as a {type.Name}.");

            var serviceDescriptor = type == asTypes.First()
                ? new ServiceDescriptor(type, implementationType, ServiceLifetime)
                : new ServiceDescriptor(type, sp => sp.GetRequiredService(_asTypes.First()), ServiceLifetime);

#if NET8_0_OR_GREATER
            if (Key is not null)
            {
                serviceDescriptor = type == asTypes.First()
                    ? new ServiceDescriptor(type, Key, implementationType, ServiceLifetime)
                    : new ServiceDescriptor(type, Key, (sp, k) => sp.GetRequiredKeyedService(_asTypes.First(), k), ServiceLifetime);
            }
#endif

            serviceCollection.Add(serviceDescriptor);
        }
    }
}
