using Core.Entities;

namespace Core.Interface
{
    public interface IProductRepository
    {
        //Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id); // New method for fetching a product by ID
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
