using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterSingletonAttribute : RegistrationAttributeBase
    {
        public RegisterSingletonAttribute()
            : base(Array.Empty<Type>(), Array.Empty<string>())
        {
        }

        public RegisterSingletonAttribute(params Type[] asTypes) 
            : base(asTypes, Array.Empty<string>())
        {
        }

        public RegisterSingletonAttribute(params string[] filters)
            : base(Array.Empty<Type>(), filters)
        {
        }

        public RegisterSingletonAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        protected override ServiceLifetime ServiceLifetime => ServiceLifetime.Singleton;
    }
}
