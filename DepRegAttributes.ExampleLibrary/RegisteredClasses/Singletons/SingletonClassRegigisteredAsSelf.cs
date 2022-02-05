namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.Singletons
{
    [RegisterSingleton]
    public class SingletonClassRegigisteredAsSelf
    {
        readonly string _id;

        public SingletonClassRegigisteredAsSelf()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void WriteSomething()
        {
            Console.WriteLine($"{nameof(SingletonClassRegigisteredAsSelf)} {_id} says hi!");
        }
    }
}
