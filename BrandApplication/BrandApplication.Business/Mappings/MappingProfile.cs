using AutoMapper;
using BrandApplication.Business.DTOs;
using BrandApplication.DataAccess.Models;

namespace BrandApplication.Business.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Model, ModelDto>().ReverseMap();
        }
    }
}
