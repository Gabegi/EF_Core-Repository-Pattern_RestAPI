using AutoMapper;
using BrandApplication.Business.Services.IServices;
using BrandApplication.DataAccess.Interfaces;
using System.Linq.Expressions;


namespace BrandApplication.Business.Services
{
    public class ReadServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public ReadServiceAsync(IGenericRepository<TEntity> genericRepository, IMapper mapper) : base()
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, bool>> filter = null)
        {
            var result = await _genericRepository.GetAllAsync(_mapper.Map<Expression<Func<TEntity, bool>>>(filter), false);
            return _mapper.Map<IEnumerable<TDto>>(result);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result = await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<TDto>(result);
        }
    }
}
