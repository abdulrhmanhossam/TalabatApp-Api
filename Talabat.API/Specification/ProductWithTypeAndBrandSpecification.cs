using Talabat.DAL.Entities;

namespace Talabat.API.Specification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        // this ctor to get all product 
        public ProductWithTypeAndBrandSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        // this ctor to get one product by id
        public ProductWithTypeAndBrandSpecification(int id):base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
