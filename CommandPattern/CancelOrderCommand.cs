using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.CommandPattern
{
    public class CancelOrderCommand : ICommand
    {
        public void Execute() => Console.WriteLine("Order canceled.");
    }
}
