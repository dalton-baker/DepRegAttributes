namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

public interface IKeyedWithInterface
{
}

[RegisterTransient<IKeyedWithInterface>(Key = "Value")]
public class KeyedWithInterface : IKeyedWithInterface
{
}
