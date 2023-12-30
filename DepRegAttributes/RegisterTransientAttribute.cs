using System;

namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class RegisterTransientAttribute(params Type[] types) : Attribute
#pragma warning restore CS9113 // Parameter is unread.
{
    public object Tag { get; set; }

    public object Key { get; set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9> : RegisterTransientAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterTransientAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : RegisterTransientAttribute
{
}