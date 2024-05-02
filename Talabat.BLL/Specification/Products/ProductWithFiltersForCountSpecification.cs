using Talabat.DAL.Entities;

namespace Talabat.BLL.Specification.Products;
public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(p =>
        (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId.Value) &&
        (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId.Value))
    {
        
    }
}