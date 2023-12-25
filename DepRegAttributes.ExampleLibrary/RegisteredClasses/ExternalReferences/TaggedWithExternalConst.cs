using DepRegAttributes.ExternalExampleLibrary.SubNamespace;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.ExternalReferences;

[RegisterTransient(Tag = ExternalConst.Value)]
public class TaggedWithExternalConst
{

}