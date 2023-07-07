using MarketPlace.Data;
using MarketPlace.Models;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IImageRepository _imageRepository;

        public ProductRepository(AppDbContext appDbContext, IImageRepository imageRepository)
        {
            _appDbContext = appDbContext;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? filterBy, string? filterQuery, string? sortBy, bool isAscending = true,
            int pageNumber = 1, int pageSize = 100)
        {
            var Products = _appDbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterQuery) && filterBy.Contains("Name", StringComparison.OrdinalIgnoreCase))
            {
                    Products = Products.Where(p => p.Name.Contains(filterQuery));
            }

            if (!string.IsNullOrEmpty(sortBy) && sortBy.Contains("Name", StringComparison.OrdinalIgnoreCase))
            {
                    Products = isAscending ? Products.OrderBy(p => p.Name) : Products.OrderByDescending(p => p.Name);
            }

            int skipResults = (pageNumber - 1) * pageSize;

            return await Products.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var Product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
            return Product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _imageRepository.UploadImage(product.Image);

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
