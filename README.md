# DBaker.DepRegAttributes
Register dependencies with attributes!


Example Usage:
```c#
[RegisterTransient]
public class TransientRegisteredAsSelf
{
    //This will be registered as TransientRegisteredAsSelf
    ...
}

[RegisterTransient]
public class TransientRegisteredAsInterface : ITransientRegisteredAsInterface
{
    //This will be registered as ITransientRegisteredAsInterface
    //This only automatically happens if the interface and class name match
    //Note: providing any parameters will override this behavior
    ...
}

[RegisterTransient(typeof(IInterface))]
public class TransientRegisteredWithInterface: IInterface
{
    //This will be registered as IInterface
    ...
}

[RegisterTransient(typeof(IInterface))]
public class TransientRegisteredWithInterface: ITransientRegisteredAsInterface, IInterface
{
    //This will be registered only as IInterface
    //Providing parameters overrides the automatic registration by interfaces with the same name
    ...
}

[RegisterTransient(typeof(IInterface1), typeof(IInterface2))]
public class TransientRegisteredWithMultipleInterface: IInterface1, IInterface2
{
    //This will be registered as IInterface1 and IInterface2
    ...
}

[RegisterSingleton(typeof(IInterface1), typeof(IInterface2))]
public class SingletonRegisteredWithMultipleInterface: IInterface1, IInterface2
{
    //This will be registered as IInterface1 and IInterface2
    //Requesting either interface from the service provider will give you the exact same object
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

[RegisterTransient(new string[] { "tag2", "tag3" }, typeof(IInterface1), typeof(IInterface2))]
public class TagExample2 : IInterface1, IInterface2
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
