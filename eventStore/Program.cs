using eventStore;



namespace eventStore
{
    public class Program
    {
        public static void Main(string[] arg)
        {
            var service = new Service();
            new Store(service).Run();
        }
    }
}