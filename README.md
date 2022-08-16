# DBaker.DepRegAttributes
Register dependencies with attributes


Example Usage:
```c#
[RegisterTransient]
public class TransientRegisteredAsSelf
{
    ...
}

[RegisterTransient(typeof(Iinterface))]
public class TransientRegisteredWithInterface: Iinterface
{
    ...
}

[RegisterTransient(typeof(Iinterface1), typeof(Iinterface2), typeof(Iinterface3))]
public class TransientRegisteredWithMultipleInterface: Iinterface1, Iinterface2, Iinterface3
{
    ...
}
```

You can register Transient, Scoped, and Singletons:
```c#
[RegisterTransient]
...

[RegisterScoped]
...

[RegisterSingleton]
...
```

In order for the attributes to work you will need to create an extension in the project you are using the attributes:
```c#
public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddExampleLibraryRegistration(this IServiceCollection services)
    {
        return services.RegisterDependenciesByAttribute();
    }
}
```

You can use tagging to filter the servicees you register. If you add no tag, your service will be registered all the time.
```c#
[RegisterTransient(new string[] { "tag1", "tag2" })]
public class TagExample1
{
    ...
}

[RegisterTransient(new string[] { "tag2", "tag3" }, typeof(Interface1), typeof(Interface2))]
public class TagExample2
{
    ...
}

//register dependencies based on tag
public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddByTag(this IServiceCollection services)
    {
        //This will get TagExample1
        return services.RegisterDependenciesByAttribute("tag1");
    }

    public static IServiceCollection AddByTag2(this IServiceCollection services)
    {
        //This will get TagExample1 and TagExample2
        return services.RegisterDependenciesByAttribute("tag2");
    }

    public static IServiceCollection AddByTag3(this IServiceCollection services)
    {
        //This will get TagExample2
        return services.RegisterDependenciesByAttribute("tag3");
    }

    public static IServiceCollection AddAll(this IServiceCollection services)
    {
        //This will get TagExample1 and TagExample2
        return services.RegisterDependenciesByAttribute();
    }
}
```

You can look at examples of usage in the DepRegAttributes.Example and DepRegAttributes.ExampleLibrary projects.
