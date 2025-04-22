

using MongoDB.Driver;
using System.Linq.Expressions;

namespace Audivia.Infrastructure.Interface
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Search(FilterDefinition<T>? filter, SortDefinition<T>? sortCondition, int? top, int? pageIndex, int? pageSize);
        Task<int> Count(FilterDefinition<T>? filter);
        Task<T?> GetById<TId>(TId id);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate);
        Task<T?> FindFirst(Expression<Func<T, bool>> predicate);
    }
}
