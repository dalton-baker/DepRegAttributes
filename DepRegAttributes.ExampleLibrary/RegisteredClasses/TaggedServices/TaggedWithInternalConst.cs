namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

[RegisterTransient(Tag = Value)]
public class TaggedWithInternalConst
{
    public const string Value = nameof(Value);

}