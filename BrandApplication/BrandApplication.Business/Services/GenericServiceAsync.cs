using AutoMapper;
using BrandApplication.Business.Services.IServices;
using BrandApplication.DataAccess.Repositories;
using System.Linq.Expressions;

namespace BrandApplication.Business.Services
{
    public class GenericServiceAsync<TEntity, TDto> : IGenericServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        private readonly GenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericServiceAsync(GenericRepository<TEntity> genericRepository, IMapper mapper) : base()
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(TDto dto)
        {
            await _genericRepository.AddAsync(_mapper.Map<TEntity>(dto));
        }

        public async Task DeleteAsync(int id)
        {
            await _genericRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, bool>> filter = null)
        {
            var result = _genericRepository.GetAllAsync(_mapper.Map<Expression<Func<TEntity, bool>>>(filter), false);
            return _mapper.Map<IEnumerable<TDto>>(result);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result =  await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<TDto>(result);
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _genericRepository.UpdateAsync(entity);
        }
    }
}
