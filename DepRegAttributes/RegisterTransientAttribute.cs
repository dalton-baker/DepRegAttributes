using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTransientAttribute : RegistrationAttributeBase
    {
        public RegisterTransientAttribute()
            : base(Array.Empty<Type>(), Array.Empty<string>())
        {
        }

        public RegisterTransientAttribute(params Type[] asTypes) 
            : base(asTypes, Array.Empty<string>())
        {
        }

        public RegisterTransientAttribute(params string[] filters)
            : base(Array.Empty<Type>(), filters)
        {
        }

        public RegisterTransientAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        protected override ServiceLifetime ServiceLifetime => ServiceLifetime.Transient;
    }
}
