namespace BrandApplication.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
