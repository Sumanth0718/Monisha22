using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost] //Http method to add the products.
        public IActionResult AddProduct(Product product)
        {
            if(product == null) 
            {
                return BadRequest("Invalid Product Details");
            }
            _productService.AddProduct(product);
            return Ok(product);
        }
        
        [HttpGet] //Http method to retrieve all the products.
        public List<Product> GetProducts()
        {
            return _productService.GetProducts();
        }
        
        [HttpGet("byid/{id}")] //Http method to retrieve a specific product by it's ID.
        public IActionResult GetbyId(int id)
        {
            var product = _productService.GetbyId(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [HttpGet("byname/{name}")] //Http method to retrieve a specific product by it's Name.
        public Product GetbyName(string name)
        {
            return _productService.GetbyName(name);
        }
        
        [HttpPut] //Http method to modify the details of a specific product.
        public IActionResult ModifyProduct(int id, Product product)
        {
            try
            {
                var existingProduct = _productService.GetbyId(id);

                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                _productService.ModifyProduct(id, product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        
        [HttpDelete("byid/{id}")] //Http method to delete a specific product by it's ID.
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var existingProduct = _productService.GetbyId(id);

                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                _productService.DeleteProduct(id);
                return Ok(id + "deleted succesfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
