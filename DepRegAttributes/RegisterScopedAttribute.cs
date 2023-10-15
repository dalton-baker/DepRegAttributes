using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterScopedAttribute : RegistrationAttributeBase
    {
        public RegisterScopedAttribute()
            : base(Array.Empty<Type>(), Array.Empty<string>())
        {
        }

        public RegisterScopedAttribute(params Type[] asTypes) 
            : base(asTypes, Array.Empty<string>())
        {
        }

        public RegisterScopedAttribute(params string[] filters)
            : base(Array.Empty<Type>(), filters)
        {
        }

        public RegisterScopedAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        public override ServiceLifetime ServiceLifetime => ServiceLifetime.Scoped;
    }
}
