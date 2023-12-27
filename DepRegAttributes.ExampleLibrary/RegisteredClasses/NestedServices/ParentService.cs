namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.NestedServices;

public class ParentService
{
    [RegisterTransient]
    public class NestedService
    {

    }

    public interface INestedServiceWithInterface { }

    [RegisterTransient]
    public class NestedServiceWithInterface : INestedServiceWithInterface
    {

    }

    public class MiddleService
    {
        [RegisterTransient]
        public class DoubleNestedService
        {

        }
    }
}
