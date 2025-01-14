using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.CommandPattern
{
    public class SaveOrderCommand : ICommand
    {
        public void Execute() => Console.WriteLine("Order saved.");
    }
}
