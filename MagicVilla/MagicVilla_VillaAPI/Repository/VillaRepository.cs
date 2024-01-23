using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VillaRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task Create(Villa villa)
        {
           await _applicationDbContext.AddAsync(villa);
           await Save();
        }

        public async Task<Villa> Get(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _applicationDbContext.Villas;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(); // query will be executed here, deffered execution
        }

        public async Task<List<Villa>> GetAll(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _applicationDbContext.Villas;

            if(filter != null)
            {
                query = query.Where(filter);
            } 
            return await query.ToListAsync(); // query will be executed here, deffered execution

        }

        public async Task Remove(Villa villa)
        {
            _applicationDbContext.Remove(villa);
            await Save();
        }

        public async Task Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
