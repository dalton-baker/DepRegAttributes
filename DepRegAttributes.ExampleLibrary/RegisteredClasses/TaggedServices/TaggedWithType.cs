namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

public interface ITaggedWithType
{
}

[RegisterTransient(types: typeof(ITaggedWithType), Tag = typeof(TypeTag))]
public class TaggedWithType : ITaggedWithType
{
}

public class TypeTag { }