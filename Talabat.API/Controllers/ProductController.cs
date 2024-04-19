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
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product>  productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
            var specification = new ProductWithTypeAndBrandSpecification();

            var products = await _productRepository
                .GetAllWithSpecificationAsync(specification);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);

            var products = await _productRepository
                .GetByIdWithSpecificationAsync(specification);

            return Ok(_mapper.Map<Product, ProductDto>(products));
        }
    }
}
