using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterSingletonAttribute : RegistrationAttributeBase
    {
        public RegisterSingletonAttribute(params Type[] asTypes) 
            : base(asTypes, new string[0])
        {
        }

        public RegisterSingletonAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        public override ServiceLifetime ServiceLifetime => ServiceLifetime.Singleton;
    }
}
