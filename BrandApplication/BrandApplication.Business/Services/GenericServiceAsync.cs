using AutoMapper;
using BrandApplication.Business.Services.IServices;
using BrandApplication.DataAccess.Interfaces;

namespace BrandApplication.Business.Services
{
    public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(TDto dto)
        {
            await _unitOfWork.Repository<TEntity>().AddAsync(_mapper.Map<TEntity>(dto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<TEntity>().DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
