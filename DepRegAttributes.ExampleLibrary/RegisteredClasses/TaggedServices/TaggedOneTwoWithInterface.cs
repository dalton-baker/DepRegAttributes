namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

public interface ITaggedOneTwoWithInterface { }

[RegisterTransient(typeof(ITaggedOneTwoWithInterface), Tag = "One")]
[RegisterTransient(typeof(ITaggedOneTwoWithInterface), Tag = "Two")]
public class TaggedOneTwoWithInterface : ITaggedOneTwoWithInterface
{
}