using AutoMapper;
using MarketPlace.Models;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var retrievedProduct = await _productRepository.GetAllAsync();

            var retrievedProductDTO = _mapper.Map<IEnumerable<ProductDTO>>(retrievedProduct);

            return Ok(retrievedProduct);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var retrievedProduct = await _productRepository.GetByIdAsync(id);

            var retrievedProductDTO = _mapper.Map<ProductDTO>(retrievedProduct);

            return Ok(retrievedProductDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTO inputDTO)
        {
            var product = _mapper.Map<Product>(inputDTO);

            var newProduct = await _productRepository.CreateAsync(product);

            _mapper.Map(newProduct, inputDTO);

            return Ok(inputDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var productToRemove = await _productRepository.DeleteAsync(name);

            if(!productToRemove)
            {
                return NotFound();
            }

            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProductDTO inputDTO)
        {
            var productToUpdate = _mapper.Map<Product>(inputDTO);
            await _productRepository.UpdateAsync(productToUpdate);

            _mapper.Map(productToUpdate, inputDTO);

            return Ok(inputDTO);
        }
    }
}
