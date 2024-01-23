using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa villa);

    }
}
