namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

[RegisterTransient(Key = new[] { "Value1", "Value2" })]
public class KeyedWithArray
{
    //Note: Though this is possible, you will not be able to actually get this service
}