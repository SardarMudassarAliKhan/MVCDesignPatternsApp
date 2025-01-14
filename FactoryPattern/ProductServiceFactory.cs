using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.FactoryPattern
{
    public class ProductServiceFactory
    {
        public IProductService GetProductService(string productType)
        {
            return productType switch
            {
                "Physical" => new PhysicalProductService(),
                "Digital" => new DigitalProductService(),
                _ => throw new ArgumentException("Invalid product type")
            };
        }
    }
}
