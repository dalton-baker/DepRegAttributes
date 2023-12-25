using DepRegAttributes.ExternalExampleLibrary.SubNamespace;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.ExternalReferences;

[RegisterTransient(Tag = ExternalEnum.Value)]
public class TaggedWithExternalEnum
{

}
