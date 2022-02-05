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


In order for the attributes to work you will need to create an extention in the project you are using the attributes:
```c#
public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddExampleLibraryRegistration(this IServiceCollection services)
    {
        return services.RegisterDependanciesByAttribute();
    }
}
```

You can look at examples of usage in the DepRegAttributes.Example and DepRegAttributes.ExampleLibrary projects.
