using MySalesStandSystem.Models;

namespace MySalesStandSystem.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
        Task<bool> UpdateProductAsync(Product product);
    }
}
