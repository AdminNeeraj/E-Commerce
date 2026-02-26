using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Infrastructure.Data;
using Core.Interface;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<ProductBrand> _brandRepository; 
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepository,
         IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository, IMapper mapper)
        {
               _repository = productRepository;
                _brandRepository = brandRepository;
                _typeRepository = typeRepository;
                _mapper = mapper;
 
        }

        // private readonly IProductRepository _repository;
        // public ProductsController(IProductRepository repository)
        // {
        //     _repository = repository;
        // }


        // GET: api/product
        [HttpGet("GetProductsList")]
        public async Task<ActionResult<IReadOnlyList<PrductsToReturnDtos>>> GetProducts()
        {
              //return Ok(await _repository.GetProductsAsync());
              //return Ok(await _repository.ListAllAsync());
                var spec = new ProductsWithTypeAndBrandsSpecification();
                var products = await _repository.ListAsync(spec);

                // using AutoMapper to map the product entity to the DTO object
                // var productsToReturn = products.Select(p => new PrductsToReturnDtos
                // {
                //     Id = p.Id,
                //     Name = p.Name,
                //     Description = p.Description,
                //     Price = p.Price,
                //     PictureUrl = p.PictureUrl,
                //     ProductType = p.ProductType.Name,
                //     ProductBrand = p.ProductBrand.Name
                // }).ToList();
                // return Ok(productsToReturn);
                //by using AutoMapper
                //return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<PrductsToReturnDtos>>(products));
                return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<PrductsToReturnDtos>>(products));
        }

        // GET: api/product/{id}
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<PrductsToReturnDtos>> GetProduct(int id)
        {  
            //var product = await _context.Products.FindAsync(id);
            //var product = await _repository.GetProductByIdAsync(id);
            // var product = await _repository.GetByIdAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }
            // return Ok(product);
            var spec = new ProductsWithTypeAndBrandsSpecification(id);
            var product = await _repository.GetEntityWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound();
            }
            //return Ok(product);

            // using AutoMapper to map the product entity to the DTO object
            // return new PrductsToReturnDtos
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductType = product.ProductType.Name,
            //     ProductBrand = product.ProductBrand.Name
            // };
            //by using AutoMapper
            return _mapper.Map<Product, PrductsToReturnDtos>(product);
        }

        // POST: api/product
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            // _context.Products.Add(product);
            // await _context.SaveChangesAsync();
            var createdProduct = await _repository.AddAsync(product);
            //var createdProduct = await _repository.AddAsync(product);
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
            //await _repository.UpdateProductAsync(product);
            await _repository.UpdateAsync(product);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            var product = await _repository.GetByIdAsync(id);
            //var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // _context.Products.Remove(product);
            // await _context.SaveChangesAsync();
            await _repository.DeleteAsync(product);
            //await _repository.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //return Ok(await _repository.GetProductBrandsAsync());
            return Ok(await _brandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //return Ok(await _repository.GetProductTypesAsync());
            return Ok(await _typeRepository.ListAllAsync());
        }

    }      
} 