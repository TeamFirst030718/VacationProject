namespace WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
           var functions = new Functions();
           functions.SendMessages().Wait();
        }
    }
}