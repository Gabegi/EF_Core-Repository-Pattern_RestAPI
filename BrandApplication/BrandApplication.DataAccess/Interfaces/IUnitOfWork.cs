namespace BrandApplication.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
