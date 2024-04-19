using Talabat.API.Specification;
using Talabat.DAL.Entities;

namespace Talabat.API.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);

    }
}
