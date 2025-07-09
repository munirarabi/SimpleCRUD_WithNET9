using CRUD_NET9.Data;
using CRUD_NET9.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_NET9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ProductModel>> SearchAllProducts()
        {
            var products = _context.Products.ToList();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductModel> SearchProductById(int id)
        {
            var product = _context.Products.Find(id);

            if(product == null)
            {
                return NotFound("No product found with the searched ID");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductModel productModel)
        {
            if(productModel == null)
            {
                return BadRequest("The product model is required.");
            }

            _context.Products.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateProduct), new {id = productModel.Id}, productModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> EditProduct(ProductModel productModel, int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound("No product found with the searched ID");
            }

            product.Name = productModel.Name;
            product.Description = productModel.Description;
            product.QuantityStock = productModel.QuantityStock;
            product.Mark = productModel.Mark;
            product.BarCode = productModel.BarCode;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound("No product found with the searched ID");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
