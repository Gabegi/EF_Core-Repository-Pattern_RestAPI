using System.Linq.Expressions;

namespace BrandApplication.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
