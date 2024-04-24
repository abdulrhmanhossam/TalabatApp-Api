using Talabat.DAL.Entities;

namespace Talabat.API.Specification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        // this ctor to get all product 
        public ProductWithTypeAndBrandSpecification(string sort, int? typeId, int? brandId)
            :base(p => 
            (!typeId.HasValue || p.ProductTypeId == typeId.Value) && 
            (!brandId.HasValue || p.ProductTypeId == brandId.Value))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort) 
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
        public ProductWithTypeAndBrandSpecification(int id):base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
