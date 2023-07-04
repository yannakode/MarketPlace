using MarketPlace.Data;
using MarketPlace.Models;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var Product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
            return Product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _appDbContext.AddAsync(product);

            await _appDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var productToRemove = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Name == name);

            if(productToRemove == null)
            {
                return false;
            }

            _appDbContext.Products.Remove(productToRemove);
            await _appDbContext.SaveChangesAsync();


            return true;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productToUpdate = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Name ==  product.Name);

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;

            _appDbContext.Update(productToUpdate);
            await _appDbContext.SaveChangesAsync();

            return productToUpdate;
        }
    }
}
