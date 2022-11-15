namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITransientClassWithOneInterface
{
}

[RegisterTransient]
public class TransientClassWithOneInterface : ITransientClassWithOneInterface
{
}