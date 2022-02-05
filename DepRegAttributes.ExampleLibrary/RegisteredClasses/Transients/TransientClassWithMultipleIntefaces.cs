namespace DepRegAttributes.ExampleLibrary;

public interface ITransientClassWithMultipleIntefaces
{
    public void WriteSomething();
}
public interface ITransientClassWithMultipleIntefaces2
{
    public void WriteSomethingAgain();
}

//Using the attribute with parameters will register the class as the types you pass in.
//You can pass in one to many types.
[RegisterTransient(typeof(ITransientClassWithMultipleIntefaces), typeof(ITransientClassWithMultipleIntefaces2))]
public class TransientClassWithMultipleIntefaces : ITransientClassWithMultipleIntefaces, ITransientClassWithMultipleIntefaces2
{
    readonly string _id;

    public TransientClassWithMultipleIntefaces()
    {
        _id = Guid.NewGuid().ToString();
    }

    public void WriteSomething()
    {
        Console.WriteLine($"{nameof(TransientClassWithMultipleIntefaces)} {_id} says hi!");
    }

    public void WriteSomethingAgain()
    {
        Console.WriteLine($"{nameof(TransientClassWithMultipleIntefaces)} {_id} says hi again!");
    }
}
