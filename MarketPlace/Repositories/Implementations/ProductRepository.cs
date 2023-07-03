using MarketPlace.Data;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;

namespace MarketPlace.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ProductDTO> GetAllAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
