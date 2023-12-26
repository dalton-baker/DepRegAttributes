using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;

[RegisterTransient(Key = TagEnum.TagExample)]
public class KeyedWithEnum
{

}
