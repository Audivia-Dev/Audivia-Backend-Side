

using System.Linq.Expressions;

namespace Audivia.Infrastructure.Interface
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById<TId>(TId id);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate);
        Task<T?> FindFirst(Expression<Func<T, bool>> predicate);
    }
}
