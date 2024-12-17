using AutoMapper;
using BrandApplication.Business.DTOs;
using BrandApplication.Business.Services.IServices.IServiceMappings;
using BrandApplication.DataAccess.Interfaces;
using BrandApplication.DataAccess.Models;

namespace BrandApplication.Business.Services.ServiceMappings
{
    public class BrandService : ReadServiceAsync<Brand, BrandDto>, IBrandService
    {
        public BrandService(IUnitOfWork unitOf, IMapper mapper) : base(unitOf, mapper)
        {
        }
    }
}
