using Microsoft.EntityFrameworkCore;
using MySalesStandSystem.Data;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;

namespace MySalesStandSystem.Repository
{
    public class ProductRepository:IProductRepository
    {
        protected readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Product> GetProducts()
        {
            return _context.products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.products.Find(id);
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            if (product is null)
            {
                return false;
            }
            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
