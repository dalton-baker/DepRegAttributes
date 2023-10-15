namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ISingletonClassWithMultipleInterfacesGeneric
{
}

public interface ISingletonClassWithMultipleInterfacesGeneric2
{
}

//A singleton class registered with multiple types will register the class with the first type.
//Then for each extra type, the service provider will use a factory method to return
//an object from the origonal type. This preserves the singleton behavior, but allows for
//interfcae segregation.
[RegisterSingleton<ISingletonClassWithMultipleInterfacesGeneric, ISingletonClassWithMultipleInterfacesGeneric2>]
public class SingletonClassWithMultipleInterfacesGeneric : 
    ISingletonClassWithMultipleInterfacesGeneric,
    ISingletonClassWithMultipleInterfacesGeneric2
{
}
