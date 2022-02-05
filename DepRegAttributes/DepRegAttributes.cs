using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepRegAttributes
{
    #region Base Class
    public abstract class RegistrationAttributeBase : Attribute
    {
        protected abstract Func<IServiceCollection, Type, Type, IServiceCollection> RegisterFirst { get; }
        protected abstract Func<IServiceCollection, Type, Func<IServiceProvider, object>, IServiceCollection> RegisterAfterFrist { get; }

        private List<Type> _asTypes;

        public RegistrationAttributeBase(params Type[] asTypes)
        {
            _asTypes = asTypes.ToList();
        }

        public void RegisterServcices(IServiceCollection serviceCollection, Type implementationType)
        {
            if (_asTypes == null || !_asTypes.Any())
            {
                _asTypes = new List<Type> { implementationType };
            }

            foreach (var type in _asTypes)
            {
                if (!type.IsAssignableFrom(implementationType))
                    throw new DepRegAttributeException($"{implementationType.Name} cannot be registered as a {type.Name}.");

                if (type == _asTypes.First())
                {
                    RegisterFirst(serviceCollection, type, implementationType);
                }
                else
                {
                    RegisterAfterFrist(serviceCollection, type, p => p.GetService(_asTypes.First()));
                }
            }
        }
    }
    #endregion


    #region Transient Registration Attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTransientAttribute : RegistrationAttributeBase
    {
        protected override Func<IServiceCollection, Type, Type, IServiceCollection> RegisterFirst =>
            ServiceCollectionServiceExtensions.AddTransient;
        protected override Func<IServiceCollection, Type, Func<IServiceProvider, object>, IServiceCollection> RegisterAfterFrist =>
            ServiceCollectionServiceExtensions.AddTransient;

        public RegisterTransientAttribute(params Type[] asTypes) : base(asTypes)
        {
        }
    }
    #endregion


    #region Scoped Registration Attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterScopedAttribute : RegistrationAttributeBase
    {
        protected override Func<IServiceCollection, Type, Type, IServiceCollection> RegisterFirst =>
            ServiceCollectionServiceExtensions.AddScoped;
        protected override Func<IServiceCollection, Type, Func<IServiceProvider, object>, IServiceCollection> RegisterAfterFrist =>
            ServiceCollectionServiceExtensions.AddScoped;

        public RegisterScopedAttribute(params Type[] asTypes) : base(asTypes)
        {
        }
    }
    #endregion


    #region Singleton Registration Attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterSingletonAttribute : RegistrationAttributeBase
    {
        protected override Func<IServiceCollection, Type, Type, IServiceCollection> RegisterFirst =>
            ServiceCollectionServiceExtensions.AddSingleton;
        protected override Func<IServiceCollection, Type, Func<IServiceProvider, object>, IServiceCollection> RegisterAfterFrist =>
            ServiceCollectionServiceExtensions.AddSingleton;

        public RegisterSingletonAttribute(params Type[] asTypes) : base(asTypes)
        {
        }
    }
    #endregion
}
