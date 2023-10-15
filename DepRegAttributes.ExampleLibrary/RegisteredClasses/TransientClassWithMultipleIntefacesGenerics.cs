namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITransientClassWithMultipleIntefacesGeneric
{
}
public interface ITransientClassWithMultipleIntefacesGeneric2
{
}

//Using the attribute with parameters will register the class as the types you pass in.
//You can pass in one to many types.
[RegisterTransient<ITransientClassWithMultipleIntefacesGeneric, ITransientClassWithMultipleIntefacesGeneric2>]
public class TransientClassWithMultipleIntefacesGeneric : 
    ITransientClassWithMultipleIntefacesGeneric, 
    ITransientClassWithMultipleIntefacesGeneric2
{
}