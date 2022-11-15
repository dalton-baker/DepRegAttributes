namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ISingletonClassWithOneInterface
{
}

[RegisterSingleton]
public class SingletonClassWithOneInterface : ISingletonClassWithOneInterface
{
}
