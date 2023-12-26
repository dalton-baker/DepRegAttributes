using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

[RegisterTransient(Key = Const.Value, Tag = TagEnum.TagExample)]
public class KeyedAndTagged
{
}
