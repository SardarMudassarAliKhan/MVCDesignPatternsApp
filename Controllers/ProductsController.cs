using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDesignPatternsApp.Models;

namespace MVCDesignPatternsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        // Simulating a database with a static list of products
        private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1000, StockQuantity = 10 },
        new Product { Id = 2, Name = "Smartphone", Price = 500, StockQuantity = 25 },
        new Product { Id = 3, Name = "Tablet", Price = 300, StockQuantity = 15 }
    };

        // GET api/products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        // GET api/products/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return Ok(product); // Return 200 and the product data
        }

        // POST api/products
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data"); // Return 400 if product is null
            }

            product.Id = products.Max(p => p.Id) + 1; // Set a new Id
            products.Add(product); // Add product to the list
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product); // Return 201 with the created product
        }

        // PUT api/products/5
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Invalid product data");
            }

            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.StockQuantity = updatedProduct.StockQuantity;

            return Ok(product); // Return 200 with updated product
        }

        // DELETE api/products/5
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }

            products.Remove(product); // Remove the product
            return Ok(); // Return 200 as a successful deletion
        }
    }
}
