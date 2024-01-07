using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepRegAttributes;

/// <summary>
/// Register a service with a Singleton lifetime.
/// </summary>
/// <param name="types">The service types for this implementation.</param>
public class RegisterSingletonAttribute(params Type[] types) : 
    RegisterAttributeBase(ServiceLifetime.Singleton, types)
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1>() : 
    RegisterSingletonAttribute(typeof(T1))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3>() :
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), 
        typeof(T6))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), 
        typeof(T6), typeof(T7))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5),
        typeof(T6), typeof(T7), typeof(T8))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), 
        typeof(T6), typeof(T7), typeof(T8), typeof(T9))
{
}

/// <inheritdoc path="/summary"/>
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() : 
    RegisterSingletonAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), 
        typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10))
{
}
