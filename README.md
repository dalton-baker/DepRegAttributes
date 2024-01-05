# Dependency Registration Attributes
Add services to your Service Collection with attributes! Never touch your Program.cs file again!

There are three attributes you can use to register services with your ServiceCollection:
```c#
[RegisterTransient]
[RegisterScoped]
[RegisterSingleton]
```

There are also IServiceCollection extensions to add these services:
```c#
serviceCollection.AddByAttribute();
serviceCollection.AddByAttribute(assembly);
```
*Passing an assembly will register services only from that assembly. If you don't pass an assembly, services from the current assembly will be registered*

## Basic Usage

```c#
[RegisterTransient]
public class ExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<ExampleService>();
}
```

If your service has an interface with a matching name, it will be automatically used.
This is the most used case. 
```c#
[RegisterTransient]
public class ExampleService : IExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<IExampleService, ExampleService>();
}
```
*Note: this does not happen if you have any explicitly defined service types in the attribute*


## Registering with Explicit Service Types

```c#
[RegisterTransient<IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
}
```

You can register your implementation with as many service types as you want.
```c#
[RegisterTransient<ExampleServiceBase, IExampleService, IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<ExampleServiceBase, ExampleService>();
    //serviceCollection.AddTransient<IExampleService, ExampleService>();
    //serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
}
```

You can also use multiple attributes.
```c#
[RegisterTransient<ExampleServiceBase>]
[RegisterTransient<IExampleService>]
[RegisterTransient<IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<ExampleServiceBase, ExampleService>();
    //serviceCollection.AddTransient<IExampleService, ExampleService>();
    //serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
}
```

If you are using a C# language version lower than 11, you will not be able to use generic arguments. Instead use type arguments:
```c#
[RegisterTransient(typeof(IExampleService), typeof(IAnotherExampleService))]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddTransient<IExampleService, ExampleService>();
    //serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
}
```

When using multiple servcie types, scoped and singletons work a little differently form transient.
For each attribute, only one object will be constructed for a singleton or scoped service.
```c#
[RegisterSingleton<IExampleService, IAnotherExampleService>]
public class ExampleService : IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddSingleton<IExampleService, ExampleService>();
    //serviceCollection.AddSingleton(sp => (IAnotherExampleService)sp.GetRequiredService<IExampleService>());
}
```
As you can see, you will get a reference to the same object when requesting either service.

If you use multiple attributes you will get a different singleton for each service.
```c#
[RegisterSingleton<IExampleService>]
[RegisterSingleton<IAnotherExampleService>]
public class ExampleService : IExampleService, IAnotherExampleService
{
    //Equivalent:
    //serviceCollection.AddSingleton<IExampleService, ExampleService>();
    //serviceCollection.AddSingleton<IAnotherExampleService, ExampleService>();
}
```

## Registration Tags
Tags can be used to register services conditionally when building your service collection.

Tags are objects, so you can use anything as long as it can be passed as an attribute argument. This limits them to constants (i.e. strings, enums, numbers).
```
serviceCollection.AddByAttribute("Example", 14, ExampleEnum.ExampleValue);
```

Tagged services will **always** be registered after untagged services, so they will override thier untagged counterparts. Untagged services will always be included, even when passing tags to the AddByAttribute function.

Define tags via the Tag property on attributes:
```c#
[RegisterTransient(Tag = "Example")]
public class ExampleService
{
    //Equivalent:
    //if(includedTag.Equals("Example"))
    //{
    //    serviceCollection.AddTransient<ExampleService>();
    //}
}
```
If you are using tags and want to register services form a specific assembly, pass the assembly as the first argument:
```
serviceCollection.AddByAttribute(assembly, "Key1", "Key2");
```


## Keyed Services
For .NET 8.0 or higher projects, you can use the new keyed services feature. You can read more about keyed services in the [.NET 8 Release Notes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#keyed-di-services).

To register a keyed service, pass a key to the `Key` property of any register attribute:
```c#
[RegisterTransient(Key = "Example")]
public class ExampleService
{
    //Equivalent:
    //serviceCollection.AddKeyedTransient<ExampleService>("Example");
}
```
