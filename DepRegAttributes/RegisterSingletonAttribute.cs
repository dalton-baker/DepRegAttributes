using System;

namespace DepRegAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class RegisterSingletonAttribute(params Type[] types) : Attribute
#pragma warning restore CS9113 // Parameter is unread.
{
    public object Tag { get; set; }

    public object Key { get; set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9> : RegisterSingletonAttribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterSingletonAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : RegisterSingletonAttribute
{
}
