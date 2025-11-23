## DepRegAttributes

**Register services with attributes. Skip manual registration in `Program.cs`!**

---

### Quick Start

**Attributes for registration:**
```c#
[RegisterTransient]
[RegisterScoped]
[RegisterSingleton]
```

**Register all attributed services:**
```c#
serviceCollection.AddByAttribute();
serviceCollection.AddByAttribute(assembly); // Only from specific assembly
```

---

## Usage Examples

### 1. Basic Registration
```c#
[RegisterTransient]
public class ExampleService { }
// Equivalent: serviceCollection.AddTransient<ExampleService>();
```

If your class implements an interface with a matching name, it will be registered as that interface:
```c#
[RegisterTransient]
public class ExampleService : IExampleService { }
// Equivalent: serviceCollection.AddTransient<IExampleService, ExampleService>();
```
*Note: Explicit service types in the attribute override this behavior.*

### 2. Explicit Service Types
```c#
[RegisterTransient<IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService { }
// Equivalent: serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
```

Register with multiple service types:
```c#
[RegisterTransient<ExampleServiceBase, IExampleService, IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService { }
// Equivalent:
// serviceCollection.AddTransient<ExampleServiceBase, ExampleService>();
// serviceCollection.AddTransient<IExampleService, ExampleService>();
// serviceCollection.AddTransient<IAnotherExampleService, ExampleService>();
```

Or use multiple attributes:
```c#
[RegisterTransient<ExampleServiceBase>]
[RegisterTransient<IExampleService>]
[RegisterTransient<IAnotherExampleService>]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService { }
// Equivalent: One registration per attribute
```

**C# < 11:** Use type arguments instead of generics:
```c#
[RegisterTransient(typeof(IExampleService), typeof(IAnotherExampleService))]
public class ExampleService : ExampleServiceBase, IExampleService, IAnotherExampleService { }
```

### 3. Singleton & Scoped Registration
For each attribute, only one object is constructed for singleton or scoped services:
```c#
[RegisterSingleton<IExampleService, IAnotherExampleService>]
public class ExampleService : IExampleService, IAnotherExampleService { }
// Equivalent:
// serviceCollection.AddSingleton<IExampleService, ExampleService>();
// serviceCollection.AddSingleton(sp => (IAnotherExampleService)sp.GetRequiredService<IExampleService>());
```
*Both interfaces resolve to the same instance.*

Multiple attributes = different instances:
```c#
[RegisterSingleton<IExampleService>]
[RegisterSingleton<IAnotherExampleService>]
public class ExampleService : IExampleService, IAnotherExampleService { }
// Equivalent: Separate singleton for each service type
```

---

## Registering Hosted Services

To register a hosted service, use:
```c#
[RegisterSingleton<IHostedService>]
public class MyWorker : IHostedService { }
// Equivalent: services.AddHostedService<MyWorker>()
```

---

## Advanced Features

### Registration Tags
Tags allow conditional registration. Tags must be constants (string, enum, number):
```c#
serviceCollection.AddByAttribute("Example");
serviceCollection.AddByAttribute(14);
serviceCollection.AddByAttribute(ExampleEnum.ExampleValue);
```

Set tags via the attribute:
```c#
[RegisterTransient(Tag = "Example")]
public class ExampleService { }
// Only registered if tag matches
```

Call `AddByAttribute` for each tag:
```c#
serviceCollection.AddByAttribute(); // Untagged
serviceCollection.AddByAttribute(ExampleEnum.ExampleValue);
serviceCollection.AddByAttribute(14);
```

### Keyed Services *(Not available in v3)*
Read more: [.NET 8 Release Notes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/runtime#keyed-di-services)

Register a keyed service:
```c#
[RegisterTransient(Key = "Example")]
public class ExampleService { }
// Equivalent: serviceCollection.AddKeyedTransient<ExampleService>("Example");
```

### Unbound Generics
Register a generic class as an unbound generic:
```c#
[RegisterTransient]
public class ExampleService<T> { }
// Equivalent: serviceCollection.AddTransient(typeof(ExampleService<>));
```

Explicit interface registration for unbound generics:
```c#
[RegisterTransient(typeof(IExampleService<>))]
public class ExampleService<T> : IExampleService<T> { }
// Equivalent: serviceCollection.AddTransient(typeof(IExampleService<>), typeof(ExampleService<>));
```
*Note: Use `typeof()` for unbound generics. No analyzer support (runtime errors only).*