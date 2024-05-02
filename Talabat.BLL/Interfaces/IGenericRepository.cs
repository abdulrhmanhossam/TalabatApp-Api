using Talabat.BLL.Specification;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);        
        Task<int> GetCountAsync(ISpecification<T> specification);
    }
}
