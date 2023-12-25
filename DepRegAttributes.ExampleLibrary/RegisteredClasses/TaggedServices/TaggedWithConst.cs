namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

public static class Const
{
    public const string Value = nameof(Value);
}

[RegisterTransient(Tag = Const.Value)]
public class TaggedWithConst
{

}
