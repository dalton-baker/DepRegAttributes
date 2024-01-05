using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute(params Type[] types) : RegisterAttributeBase(types)
{
    protected override ServiceLifetime ServiceLifetime => ServiceLifetime.Singleton;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1>() : 
    RegisterSingletonAttribute(typeof(T1))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3>() :
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9))
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10))
{
}
