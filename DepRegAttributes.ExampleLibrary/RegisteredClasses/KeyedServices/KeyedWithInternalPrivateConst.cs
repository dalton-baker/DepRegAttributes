namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

[RegisterTransient(Key = Value)]
public class KeyedWithInternalPrivateConst
{
    private const string Value = nameof(Value);
}