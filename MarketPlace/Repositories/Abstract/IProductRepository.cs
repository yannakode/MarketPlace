using MarketPlace.Models;
using MarketPlace.Models.DTO;

namespace MarketPlace.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product Product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string name);
    }
}
