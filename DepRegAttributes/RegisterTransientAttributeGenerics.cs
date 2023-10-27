using System;

#if NET7_0
namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1) };

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

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };

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
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };

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
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };

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
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9) };

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
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : RegisterTransientAttribute
{
    private static readonly Type[] _types =
        new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10) };

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
