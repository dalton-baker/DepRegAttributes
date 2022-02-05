namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.Transients
{
    //Using the attribute without parameters will register
    //this class as itself
    [RegisterTransient]
    public class TransientClassRegisteredAsSelf
    {
        readonly string _id;

        public TransientClassRegisteredAsSelf()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void WriteSomething()
        {
            Console.WriteLine($"{nameof(TransientClassRegisteredAsSelf)} {_id} says hi!");
        }
    }
}
