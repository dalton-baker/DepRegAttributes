using System;

#if NET7_0
namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1) };

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

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };

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
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };

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
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };

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
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9) };

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
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : RegisterSingletonAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10) };

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
