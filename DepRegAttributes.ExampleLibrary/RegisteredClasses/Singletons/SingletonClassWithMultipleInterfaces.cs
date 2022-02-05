namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.Singletons
{
    public interface ISingletonClassWithMultipleInterfaces
    {
        public void WriteSomething();
    }

    public interface ISingletonClassWithMultipleInterfaces2
    {
        public void WriteSomethingAgain();
    }

    //A singleton class registered with multiple types will register the class with the first type.
    //Then for each extra type, the service provider will use a factory method to return
    //an object from the origonal type. This preserves the singleton behavior, but allows for
    //interfcae segregation.
    [RegisterSingleton(typeof(ISingletonClassWithMultipleInterfaces), typeof(ISingletonClassWithMultipleInterfaces2))]
    public class SingletonClassWithMultipleInterfaces : ISingletonClassWithMultipleInterfaces, ISingletonClassWithMultipleInterfaces2
    {
        readonly string _id;

        public SingletonClassWithMultipleInterfaces()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void WriteSomething()
        {
            Console.WriteLine($"{nameof(SingletonClassWithMultipleInterfaces)} {_id} says hi!");
        }

        public void WriteSomethingAgain()
        {
            Console.WriteLine($"{nameof(SingletonClassWithMultipleInterfaces)} {_id} says hi again!");
        }
    }
}
