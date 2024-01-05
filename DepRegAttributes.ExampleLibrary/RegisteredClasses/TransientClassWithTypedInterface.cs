namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITransientClassWithTypedInterface<T>
{
}

[RegisterTransient<ITransientClassWithTypedInterface<string>>]
public class TransientClassWithTypedInterface : ITransientClassWithTypedInterface<string>
{
}