using Talabat.DAL.Entities;

namespace Talabat.BLL.Specification.Products;
public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(p =>
        (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) &&  
        (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId.Value) &&
        (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId.Value))
    {
        
    }
}