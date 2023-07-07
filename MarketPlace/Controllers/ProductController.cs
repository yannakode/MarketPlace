using AutoMapper;
using MarketPlace.Models;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public ProductController(IProductRepository productRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterBy, [FromQuery] string? filterQuery, [FromQuery] string? sortBy,
            [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var retrievedProduct = await _productRepository.GetAllAsync(filterBy, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            var retrievedProductDTO = _mapper.Map<IEnumerable<ProductDTO>>(retrievedProduct);

            return Ok(retrievedProduct);
        }

        [HttpGet]
        [Route("{id:Guide}")]
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

            if (!_imageRepository.ValidateFile(product.Image))
            {
                ModelState.AddModelError("", "The file must be smaller than 10MG and in JPG, JPEG or PNG format.");
                return BadRequest(ModelState);
            };

            var newProduct = await _productRepository.CreateAsync(product);

            _mapper.Map(newProduct, inputDTO);

            return Ok(inputDTO);
        }

        [HttpPut]
        [Route("{id:Guide}")]
        public async Task<IActionResult> Edit(ProductDTO inputDTO)
        {
            var productToUpdate = _mapper.Map<Product>(inputDTO);
            await _productRepository.UpdateAsync(productToUpdate);

            _mapper.Map(productToUpdate, inputDTO);

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
    }
}
