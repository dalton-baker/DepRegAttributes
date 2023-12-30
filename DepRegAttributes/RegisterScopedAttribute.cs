using System;

namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class RegisterScopedAttribute(params Type[] types) : Attribute
#pragma warning restore CS9113 // Parameter is unread.
{
    public object Tag { get; set; }

    public object Key { get; set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5, T6> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5, T6, T7> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5, T6, T7, T8> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9> : RegisterScopedAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterScopedAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : RegisterScopedAttribute
{
}
