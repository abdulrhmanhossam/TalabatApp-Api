using Talabat.DAL.Entities;

namespace Talabat.BLL.Specification.Products;

public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
{
    // this ctor to get all product 
    public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams)
        : base(p =>
        (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId.Value) &&
        (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId.Value))
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        AddOrderBy(p => p.Name);

        ApplyPagination((int)(productParams.PageSize * (productParams.PageIndex - 1)), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }

    // this ctor to get one product by id
    public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
