using AutoMapper;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Source -> Target
            CreateMap<Models.Villa, Models.Dto.VillaDTO>().ReverseMap();
            CreateMap<Models.Villa, Models.Dto.VillaCreateDTO>().ReverseMap();
            CreateMap<Models.Villa, Models.Dto.VillaUpdateDTO>().ReverseMap();
        }
    }
}
