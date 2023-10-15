using System;

#if NET7_0
namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T> : RegisterScopedAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T) };

    public RegisterScopedAttribute()
        : base(_types)
    {
    }

    public RegisterScopedAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2> : RegisterScopedAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2) };

    public RegisterScopedAttribute()
        : base(_types)
    {
    }

    public RegisterScopedAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3> : RegisterScopedAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3) };

    public RegisterScopedAttribute()
        : base(_types)
    {
    }

    public RegisterScopedAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4> : RegisterScopedAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

    public RegisterScopedAttribute()
        : base(_types)
    {
    }

    public RegisterScopedAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5> : RegisterScopedAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };

    public RegisterScopedAttribute()
        : base(_types)
    {
    }

    public RegisterScopedAttribute(params string[] filters)
        : base(filters, _types)
    {
    }
}
#endif
