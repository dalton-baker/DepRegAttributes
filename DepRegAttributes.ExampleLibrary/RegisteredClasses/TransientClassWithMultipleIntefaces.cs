namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITransientClassWithMultipleIntefaces
{
}
public interface ITransientClassWithMultipleIntefaces2
{
}

//Using the attribute with parameters will register the class as the types you pass in.
//You can pass in one to many types.
[RegisterTransient(typeof(ITransientClassWithMultipleIntefaces), typeof(ITransientClassWithMultipleIntefaces2))]
public class TransientClassWithMultipleIntefaces : ITransientClassWithMultipleIntefaces, ITransientClassWithMultipleIntefaces2
{
}