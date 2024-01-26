using System.Linq.Expressions;

namespace BrandApplication.Business.Services.IServices
{
    public interface IReadServiceAsync<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, bool>> filter = null);
        Task<TDto> GetByIdAsync(int id);
    }
}
