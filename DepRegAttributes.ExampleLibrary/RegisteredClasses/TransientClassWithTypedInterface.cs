namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITransientClassWithTypedInterface<T>
{
}

[RegisterTransient<ITransientClassWithTypedInterface<string>>]
public class TransientClassWithTypedInterface : ITransientClassWithTypedInterface<string>
{
}



public interface ITransientOpenGeneric<T>
{
}

[RegisterTransient]
public class TransientOpenGeneric<T> : ITransientOpenGeneric<T>
{
}