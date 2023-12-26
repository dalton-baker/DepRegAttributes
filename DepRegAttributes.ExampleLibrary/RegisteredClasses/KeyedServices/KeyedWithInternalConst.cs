namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

[RegisterTransient(Key = Value)]
public class KeyedWithInternalConst
{
    public const string Value = nameof(Value);
}