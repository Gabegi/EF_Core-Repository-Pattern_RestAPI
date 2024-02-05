using AutoMapper;
using BrandApplication.Business.Services.IServices;
using BrandApplication.DataAccess.Repositories;

namespace BrandApplication.Business.Services
{
    public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        private readonly GenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericServiceAsync(GenericRepository<TEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
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

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _genericRepository.UpdateAsync(entity);
        }
    }
}
