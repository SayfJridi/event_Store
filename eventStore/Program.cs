using eventStore;



namespace eventStore
{
    public class Program
    {
        public static void Main(string[] arg)
        {
            var service = new Service(new EventStoreService());
            new Store(service).Run();
        }
    }
}