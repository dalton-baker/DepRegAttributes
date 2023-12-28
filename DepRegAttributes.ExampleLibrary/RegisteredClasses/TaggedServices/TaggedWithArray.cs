namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

[RegisterTransient(Tag = new[] { "Value1", "Value2" }, Key = new[] { "Value1", "Value2" })]
internal class TaggedWithArray
{
    //Note: Though this is possible, you will not be able to actually get this service
}