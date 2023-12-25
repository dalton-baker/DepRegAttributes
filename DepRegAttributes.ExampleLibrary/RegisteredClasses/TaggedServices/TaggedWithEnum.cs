namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

public enum TagEnum
{
    TagExample
}

[RegisterTransient(Tag = TagEnum.TagExample)]
public class TaggedWithEnum
{

}
