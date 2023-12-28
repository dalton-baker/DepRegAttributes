using DepRegAttributes.ExampleLibrary.Test;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses
{
    [RegisterTransient<ITransientClassWithTypedInterface<ITypeInOtherNamespace>>]
    public class TransientClassWithTypedInterfaceInOtherNamespace : ITransientClassWithTypedInterface<ITypeInOtherNamespace>
    {
    }
}

namespace DepRegAttributes.ExampleLibrary.Test
{
    public interface ITypeInOtherNamespace
    {
    }
}