using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();  
            return product;
        }

        public Task DeleteProductAsync(int id)
        {
           if (!_context.Products.Any(p => p.Id == id))
           {
                throw new KeyNotFoundException($"Product with id {id} not found.");
           }
              var product = new Product { Id = id };
                _context.Products.Remove(product);
                return _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.AsAsyncEnumerable().ToListAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
           //return _context.Products.FindAsync(id).AsTask();
            return _context.Products.
                Include(p => p.ProductType).
                Include(p => p.ProductBrand).
                FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {   
            //return await _context.Products.AsAsyncEnumerable().ToListAsync();
            return await _context.Products.
                Include(p => p.ProductType).
                Include(p => p.ProductBrand).
                AsAsyncEnumerable().ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public Task UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}