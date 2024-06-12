using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specification.Products;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository; 
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product>  productRepository, 
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
        public async Task<ActionResult<Pagination<ProductDto>>> Products([FromQuery]ProductSpecParams productParams)
        {
            var specification = new ProductWithTypeAndBrandSpecification(productParams);

            var products = await _productRepository
                .GetAllWithSpecificationAsync(specification);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            if (data == null)
                return NoContent();

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var count = await _productRepository.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductDto>((int)productParams.PageIndex, productParams.PageSize, count, data));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Product(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);

            var products = await _productRepository
                .GetByIdWithSpecificationAsync(specification);

            var productDto = _mapper.Map<Product, ProductDto>(products);
            if (productDto == null)
                return NotFound(new ApiResponse(404));

            return Ok(productDto);
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> Brands()
        {
            var brands = await _brandRepository.GetAllAsync();

            if (brands == null)
                return NoContent();

            return Ok(brands);
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> Types()
        {
            var types = await _typeRepository.GetAllAsync();

            if (types == null)
                return NoContent();

            return Ok(types);
        }
    }
}
