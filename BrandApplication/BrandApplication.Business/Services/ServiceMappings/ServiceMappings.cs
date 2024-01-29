using AutoMapper;
using BrandApplication.Business.DTOs;
using BrandApplication.Business.Services.IServices.IServiceMappings;
using BrandApplication.DataAccess.Interfaces;
using BrandApplication.DataAccess.Models;

namespace BrandApplication.Business.Services.ServiceMappings
{
    public class BrandMapping : ReadServiceAsync<Brand, BrandDto>, IBrandMapping
    {
        public BrandMapping(IGenericRepository<Brand> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
