namespace MVCDesignPatternsApp.IAppRepository
{
    public interface ICommand
    {
        void Execute();
    }

    

    public class CancelOrderCommand : ICommand
    {
        public void Execute() => Console.WriteLine("Order canceled.");
    }
}
