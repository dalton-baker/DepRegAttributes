namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.OpenGenerics;

public interface ITransientOpenMultipleGeneric<T1, T2>
{
}

[RegisterTransient(typeof(ITransientOpenMultipleGeneric<,>))]
[RegisterTransient]
public class TransientOpenMultipleGeneric<T1, T2> : ITransientOpenMultipleGeneric<T1, T2>
{
}