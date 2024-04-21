using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Interfaces;
using Talabat.API.Specification;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository; 
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product>  productRepository, 
            IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts(string sort)
        {
            var specification = new ProductWithTypeAndBrandSpecification(sort);

            var products = await _productRepository
                .GetAllWithSpecificationAsync(specification);

            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            if (productsDto == null)
                return NoContent();

            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);

            var products = await _productRepository
                .GetByIdWithSpecificationAsync(specification);

            var productDto = _mapper.Map<Product, ProductDto>(products);
            if (productDto == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepository.GetAllAsync();

            if (brands == null)
                return NoContent();

            return Ok(brands);
        }

        //[HttpGet("brand/{id:int}")]
        //public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        //{
        //    var brand = await _brandRepository.GetByIdAsync(id);

        //    if (brand == null)
        //    {
        //        return StatusCode(404, "Resource was not Found");
        //    }

        //    return Ok(brand);
        //}

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typeRepository.GetAllAsync();

            if (types == null)
                return NoContent();

            return Ok(types);
        }

        //[HttpGet("type/{id:int}")]
        //public async Task<ActionResult<ProductType>> GetType(int id)
        //{
        //    var type = await _typeRepository.GetByIdAsync(id);

        //    if (type == null)
        //    {
        //        return StatusCode(404, "Resource was not Found");
        //    }

        //    return Ok(type);
        //}
    }
}
