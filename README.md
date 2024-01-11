Add services to your Service Collection with attributes! Never touch your Program.cs file again!

*Note: This version targets the lowest version of Microsoft.Extensions.DependencyInjection.Abstractions possible (3.1.32). This means Keyed services are not available.*

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

Untagged services will always be included, even when passing tags to the AddByAttribute function. Services are registered in the same order as their attributes, keep this in mind if you have a service that is registered as tagged and untagged. You will want the tagged attribute bellow the untagged attribute, since the untagged attribute will be included all the time.

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

## Unbound Generics
You can register a generic class as an unbound generic:
```c#
[RegisterTransient]
public class ExampleService<T>
{
    //Equivalent:
    //serviceCollection.AddTransient(typeof(ExampleService<>));
}
```

Unbound generics do not automatically register matching interface types, so you will have to do it manually:
```c#
[RegisterTransient(typeof(IExampleService<>))]
public class ExampleService<T> : IExampleService<T>
{
    //Equivalent:
    //serviceCollection.AddTransient(typeof(IExampleService<>), typeof(ExampleService<>));
}
```
*Note: You must use `typeof()` arguments when doing this, you cannot pass an unbound generic as a generic argument.*
*Note 2: There is no analyzer support for unbound generics, failures will only appear at runtime.*