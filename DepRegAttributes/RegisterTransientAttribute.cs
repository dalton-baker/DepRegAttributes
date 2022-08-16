using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTransientAttribute : RegistrationAttributeBase
    {
        public RegisterTransientAttribute(params Type[] asTypes) 
            : base(asTypes, new string[0])
        {
        }

        public RegisterTransientAttribute(string[] filters, params Type[] asTypes) 
            : base(asTypes, filters)
        {
        }

        public override ServiceLifetime ServiceLifetime => ServiceLifetime.Transient;
    }
}
