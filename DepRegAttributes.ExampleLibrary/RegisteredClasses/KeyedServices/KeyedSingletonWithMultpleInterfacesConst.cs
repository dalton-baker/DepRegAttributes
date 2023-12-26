using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

public interface IKeyedInterface
{
}

public interface IKeyedInterface2
{
}

[RegisterSingleton<IKeyedInterface, IKeyedInterface2>(Key = Const.Value)]
public class KeyedSingletonWithMultpleInterfaces: IKeyedInterface, IKeyedInterface2
{

}