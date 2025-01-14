using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDesignPatternsApp.FactoryPattern;

namespace MVCDesignPatternsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ProductServiceFactory _factory;

        public OrdersController(ProductServiceFactory factory)
        {
            _factory = factory;
        }

        public void CreateOrder(string productType)
        {
            var service = _factory.GetProductService(productType);
            service.ProcessOrder();
        }
    }
}
