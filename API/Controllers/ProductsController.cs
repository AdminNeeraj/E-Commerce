using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Infrastructure.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }


        // GET: api/product
        [HttpGet("GetProductsList")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // TODO: Implement database query
           // return Ok(new List<Product>());
              return Ok(await _context.Products.ToListAsync());
        }

        // GET: api/product/{id}
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {  
            // return Ok(new Product());
            var product = await _context.Products.FindAsync(id);
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
            // TODO: Implement product creation
            //return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
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
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // TODO: Implement product deletion>>
            //return NoContent();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
       }

    
}