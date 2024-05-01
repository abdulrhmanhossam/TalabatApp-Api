namespace Talabat.BLL.Specification.Products;
public class ProductSpecParams
{
    private const int _maxPageSize = 50;
    public int? PageIndex { get; set; } = 1;
    private int pageSize = 5;
    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > _maxPageSize ? _maxPageSize : value; }
    }
    
    public string Sort { get; set; }
    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
}