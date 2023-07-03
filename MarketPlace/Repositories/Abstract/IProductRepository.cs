using MarketPlace.Models.DTO;

namespace MarketPlace.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<ProductDTO> GetAllAsync(ProductDTO productDTO);
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);
        Task<ProductDTO> UpdateAsync(Guid id);
        Task<ProductDTO> DeleteAsync(Guid id);
    }
}
