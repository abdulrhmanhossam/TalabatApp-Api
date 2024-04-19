using System.Linq.Expressions;

namespace Talabat.API.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = 
            new List<Expression<Func<T, object>>>();
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }
        public void AddInclude(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include);
        }
    }
}
