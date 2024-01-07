namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.OpenGenerics;

public interface ITransientOpenGeneric<T>
{
}

[RegisterTransient(typeof(ITransientOpenGeneric<>))]
[RegisterTransient]
public class TransientOpenGeneric<T>: ITransientOpenGeneric<T>
{
}
