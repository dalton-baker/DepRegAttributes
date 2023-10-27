# Dependency Registration Attributes
Register dependencies with attributes and never touch your Program.cs file again!


### Example usage in .NET Standard 2.0:
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


## Advanced Scenarios

### Registering grouped Singleton/Scoped
This will allow you to have two singletons in the service provider based on interfaces. For example, requesting ISingletonGroup1 or ISingletonGroup1_2 will give you the same object.
Requesting ISingletonGroup2 or ISingletonGroup2_2 will give you the same object, but it will be different from the ISingletonGroup1 object
```c#
[RegisterSingleton<ISingletonGroup1, ISingletonGroup1_2>]
[RegisterSingleton<ISingletonGroup2, ISingletonGroup2_2>]
public class SingletonClassGroupedByIterfaces: 
    ISingletonGroup1, 
    ISingletonGroup1_2, 
    ISingletonGroup2, 
    ISingletonGroup2_2
{
}

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

### Implementing a factory
This is a very limited concept, but might be useful. It's possible new verison of C# will make this significantly more useful.
For now, you can assign a value to the base `ObjectFactory` property. It looks like this:

`protected virtual Func<IServiceProvider, Type, object> ObjectFactory { get; set; }`

Again this is extremely limited, but I added it because I wanted to have an easy way to bind objects to configuration in ASP.NET Core. Here is an example:
```c#
[AttributeUsage(AttributeTargets.Class)]
private class RegisterConfigAttribute : RegisterSingletonAttribute
{
    public RegisterConfigAttribute(string configKey)
    {
        ObjectFactory = (sp, t) =>
        {
            var config = Activator.CreateInstance(t);
            sp.GetRequiredService<IConfiguration>().Bind(configKey, config);
            return config;
        };
    }
}
```
This will have the effect of registering a singleton object in the service provider. Additionally it will automatically bind the object to a section in your appsettings.json file, determined by the configKey.

Then we can use it on a class like this:
```c#
public interface IConfigClass
{
    string Example { get; }
}

[RegisterConfig("Config")]
public class ConfigClass : IConfigClass
{
    public string Example { get; set; } = string.Empty;
}
```

If the following is in your appsettings.json file, the "Config" section will be automatically be mapped to the IConfigClass.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*",
  "Config": {
    "Example": "A Value"
  }
}
```





You can look at examples of usage in the DepRegAttributes.Example and DepRegAttributes.ExampleLibrary projects.
