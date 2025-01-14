using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.FactoryPattern
{
    public class PhysicalProductService : IProductService
    {
        public void ProcessOrder() => Console.WriteLine("Processing physical product order.");
    }
}
