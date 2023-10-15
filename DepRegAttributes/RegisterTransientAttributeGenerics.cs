using System;

#if NET7_0
namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T) };

    public RegisterTransientAttribute()
        : base(_types)
    {
    }

    public RegisterTransientAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1, T2> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2) };

    public RegisterTransientAttribute()
        : base(_types)
    {
    }

    public RegisterTransientAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1, T2, T3> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3) };

    public RegisterTransientAttribute()
        : base(_types)
    {
    }

    public RegisterTransientAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1, T2, T3, T4> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

    public RegisterTransientAttribute()
        : base(_types)
    {
    }

    public RegisterTransientAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };

    public RegisterTransientAttribute()
        : base(_types)
    {
    }

    public RegisterTransientAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}
#endif
