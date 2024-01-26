namespace BrandApplication.Business.Services.IServices
{
    public interface IGenericServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class 
        
    {
        Task AddAsync(TDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(TDto dto);
    }
}
