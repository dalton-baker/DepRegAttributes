using DepRegAttributes.ExternalExampleLibrary;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.ExternalReferences;

[RegisterTransient<IExternalInterface>]
public class ExternalInterface : IExternalInterface
{

}
