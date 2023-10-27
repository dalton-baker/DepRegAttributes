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

        protected abstract ServiceLifetime ServiceLifetime { get; }
        protected virtual Func<IServiceProvider, Type, object> ObjectFactory { get; set; } = null;

        public void RegisterServices(IServiceCollection serviceCollection, Type implementationType, string filter = null)
        {
            if (filter != null && _tags.Any() && !_tags.Contains(filter))
                return;

            var asTypes = _asTypes == null || !_asTypes.Any()
                ? new Type[] { implementationType.GetInterface($"I{implementationType.Name}") ?? implementationType }
                : _asTypes;

            foreach (var type in asTypes)
            {
                if (!type.IsAssignableFrom(implementationType))
                    throw new DepRegAttributeException($"{implementationType.Name} cannot be registered as a {type.Name}.");

                var descriptor = type == asTypes.First()
                    ? ObjectFactory == null 
                        ? new ServiceDescriptor(type, implementationType, ServiceLifetime)
                        : new ServiceDescriptor(type, sp => ObjectFactory(sp, implementationType), ServiceLifetime)
                    : new ServiceDescriptor(type, p => p.GetService(_asTypes.First()), ServiceLifetime);

                serviceCollection.Add(descriptor);
            }
        }
    }
}
