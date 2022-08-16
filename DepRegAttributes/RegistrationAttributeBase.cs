using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DepRegAttributes
{
    public abstract class RegistrationAttributeBase : Attribute
    {
        private readonly Type[] _asTypes;
        private readonly string[] _tags;

        public RegistrationAttributeBase(Type[] asTypes, string[] tags)
        {
            _asTypes = asTypes;
            _tags = tags;
        }

        public abstract ServiceLifetime ServiceLifetime { get; }

        public void RegisterServices(IServiceCollection serviceCollection, Type implementationType, string filter = null)
        {
            if (filter != null && _tags.Any() && !_tags.Contains(filter))
                return;

            var asTypes = _asTypes == null || !_asTypes.Any()
                ? new Type[] { implementationType }
                : _asTypes;

            foreach (var type in asTypes)
            {
                if (!type.IsAssignableFrom(implementationType))
                    throw new DepRegAttributeException($"{implementationType.Name} cannot be registered as a {type.Name}.");

                var descriptor = type == asTypes.First()
                    ? new ServiceDescriptor(type, implementationType, ServiceLifetime)
                    : new ServiceDescriptor(type, p => p.GetService(_asTypes.First()), ServiceLifetime);

                serviceCollection.Add(descriptor);
            }
        }
    }
}
