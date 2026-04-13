using StudentManagement.Models;
using System.Linq.Expressions;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetWithIncludeAsync(
            Func<IQueryable<T>, IQueryable<T>> include,
            Expression<Func<T, bool>>? predicate = null);
        Task<List<TResult>> SelectAsync<TResult>(
    Expression<Func<T, bool>>? predicate,
    Expression<Func<T, TResult>> selector);

        Task<T?> GetByIdAsync(int id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
