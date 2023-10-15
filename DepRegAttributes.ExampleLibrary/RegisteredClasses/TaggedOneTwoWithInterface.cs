namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ITaggedOneTwoWithInterface { }

[RegisterTransient(new[] { "One", "Two" }, typeof(ITaggedOneTwoWithInterface))]
public class TaggedOneTwoWithInterface : ITaggedOneTwoWithInterface
{
}