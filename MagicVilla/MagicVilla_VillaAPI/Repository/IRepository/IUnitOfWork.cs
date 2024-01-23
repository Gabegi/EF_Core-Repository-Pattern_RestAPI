namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        Task SaveAsync();
    }
}
