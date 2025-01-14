using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.FactoryPattern
{
    public class DigitalProductService : IProductService
    {
        public void ProcessOrder() => Console.WriteLine("Processing digital product order.");
    }
}
