using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterScopedAttribute : RegistrationAttributeBase
    {
        public RegisterScopedAttribute(params Type[] asTypes) 
            : base(asTypes, new string[0])
        {
        }

        public RegisterScopedAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        public override ServiceLifetime ServiceLifetime => ServiceLifetime.Scoped;
    }
}
