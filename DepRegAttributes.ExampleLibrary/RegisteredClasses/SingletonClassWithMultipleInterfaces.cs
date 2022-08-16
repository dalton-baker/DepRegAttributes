namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ISingletonClassWithMultipleInterfaces
{
}

public interface ISingletonClassWithMultipleInterfaces2
{
}

//A singleton class registered with multiple types will register the class with the first type.
//Then for each extra type, the service provider will use a factory method to return
//an object from the origonal type. This preserves the singleton behavior, but allows for
//interfcae segregation.
[RegisterSingleton(typeof(ISingletonClassWithMultipleInterfaces), typeof(ISingletonClassWithMultipleInterfaces2))]
public class SingletonClassWithMultipleInterfaces : ISingletonClassWithMultipleInterfaces, ISingletonClassWithMultipleInterfaces2
{
}
