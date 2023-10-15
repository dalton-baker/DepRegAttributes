using System;

#if NET7_0
namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T) };

    public RegisterSingletonAttribute()
        : base(_types)
    {
    }

    public RegisterSingletonAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2) };

    public RegisterSingletonAttribute()
        : base(_types)
    {
    }

    public RegisterSingletonAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3) };

    public RegisterSingletonAttribute()
        : base(_types)
    {
    }

    public RegisterSingletonAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

    public RegisterSingletonAttribute()
        : base(_types)
    {
    }

    public RegisterSingletonAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };

    public RegisterSingletonAttribute()
        : base(_types)
    {
    }

    public RegisterSingletonAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}
#endif
