using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Infrastructure.Data;
using Core.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }


        // GET: api/product
        [HttpGet("GetProductsList")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
              return Ok(await _repository.GetProductsAsync());
        }

        // GET: api/product/{id}
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {  
            //var product = await _context.Products.FindAsync(id);
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/product
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            // _context.Products.Add(product);
            // await _context.SaveChangesAsync();
            var createdProduct = await _repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/product/{id}
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // TODO: Implement product update
            //return NoContent();
            if (id != product.Id)
            {
                return BadRequest();
            }
            // _context.Entry(product).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            await _repository.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // _context.Products.Remove(product);
            // await _context.SaveChangesAsync();
            await _repository.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repository.GetProductTypesAsync());
        }

    }      
}