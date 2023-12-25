namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

[RegisterTransient(Tag = Value)]
public class TaggedWithInternalPrivateConst
{
    private const string Value = nameof(Value);
}