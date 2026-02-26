using System.Text.Json;
using Core.Entities;

namespace Core.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var ProductBrands = File.ReadAllText("Data/SeedData/brand.json");

                //var ProductBrands = File.ReadAllText("../Core.Infrastructure/Data/SeedData/brand.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrands);

                context.ProductBrands.AddRange(Brands);
                // foreach (var productBrand in Brands)
                // {
                //     //context.ProductBrands.Add(productBrand);
                //     context.ProductBrands.AddRange(productBrand);
                // }
               // await context.SaveChangesAsync();
            }

             if (!context.ProductTypes.Any())
            {
                var ProductTypes = File.ReadAllText("../Core.Infrastructure/Data/SeedData/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypes);

                context.ProductTypes.AddRange(Types);
                // foreach (var productType in Types)
                // {
                //     //context.ProductTypes.Add(productType);
                //     context.ProductTypes.AddRange(productType);
                // }
                //await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Core.Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                context.Products.AddRange(products);
                // foreach (var product in products)
                // {
                //     //context.Products.Add(product);
                //     context.Products.AddRange(product);
                // }

                //await context.SaveChangesAsync();
            }
            if(context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }

            Console.WriteLine(Directory.GetCurrentDirectory());

        }
    }
}