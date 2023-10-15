# DBaker.DepRegAttributes
Register dependencies with attributes!


Example usage in .NET Standard 2.0:
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

[RegisterTransient(typeof(IInterface))] //.NET Standard
[RegisterTransient<IInterface>] //In .NET 7 or later
public class TransientRegisteredWithInterface: IInterface
{
    //This will be registered as IInterface
    ...
}

[RegisterTransient(typeof(IInterface))] //.NET Standard
[RegisterTransient<IInterface>] //In .NET 7 or later
public class TransientRegisteredWithInterface: ITransientRegisteredAsInterface, IInterface
{
    //This will be registered only as IInterface
    //Providing parameters overrides the automatic registration by interfaces with the same name
    ...
}

[RegisterTransient(typeof(IInterface1), typeof(IInterface2))] //.NET Standard
[RegisterTransient<IInterface1, IInterface2>] //In .NET 7 or later
public class TransientRegisteredWithMultipleInterface: IInterface1, IInterface2
{
    //This will be registered as IInterface1 and IInterface2
    ...
}

[RegisterSingleton(typeof(IInterface1), typeof(IInterface2))] //.NET Standard
[RegisterTransient<IInterface1, IInterface2>] //In .NET 7 or later
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
[RegisterTransient("tag1", "tag2")]
public class TagExample1
{
    ...
}

[RegisterTransient(new[] { "tag2", "tag3" }, typeof(IInterface1), typeof(IInterface2))]
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


Advanced Scenarios
```c#
//Register Singleton or Scoped services in groups
[RegisterSingleton<ISingletonGroup1, ISingletonGroup1_2>]
[RegisterSingleton<ISingletonGroup2, ISingletonGroup2_2>]
public class SingletonClassGroupedByIterfaces: 
    ISingletonGroup1, 
    ISingletonGroup1_2, 
    ISingletonGroup2, 
    ISingletonGroup2_2
{
}

//This will allow you to have two singletons in the service provider based on interfaces.
//For example, requesting ISingletonGroup1 or ISingletonGroup1_2 will give you the same object.
//Requesting ISingletonGroup2 or ISingletonGroup2_2 will give you the same object, 
//but it will be different from the ISingletonGroup1 object

[TestMethod]
    public void GetSingletonGroupsTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singletonGroup1 = sut.GetRequiredService<ISingletonGroup1>();
        var singletonGroup1_2 = sut.GetRequiredService<ISingletonGroup1_2>();
        var singletonGroup2 = sut.GetRequiredService<ISingletonGroup2>();
        var singletonGroup2_2 = sut.GetRequiredService<ISingletonGroup2_2>();

        //Assert
        Assert.IsNotNull(singletonGroup1);
        Assert.IsNotNull(singletonGroup1_2);
        Assert.AreEqual(singletonGroup1, singletonGroup1_2);

        Assert.IsNotNull(singletonGroup2);
        Assert.IsNotNull(singletonGroup2_2);
        Assert.AreEqual(singletonGroup2, singletonGroup2_2);

        Assert.AreNotEqual(singletonGroup1, singletonGroup2);
        Assert.AreNotEqual(singletonGroup1, singletonGroup2_2);
        Assert.AreNotEqual(singletonGroup1_2, singletonGroup2);
        Assert.AreNotEqual(singletonGroup1_2, singletonGroup2_2);
    }
```

You can look at examples of usage in the DepRegAttributes.Example and DepRegAttributes.ExampleLibrary projects.
