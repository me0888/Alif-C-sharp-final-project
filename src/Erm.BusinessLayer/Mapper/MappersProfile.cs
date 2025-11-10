using AutoMapper;

using Erm.DataAccess.Models;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Mapper;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RiskProfileDTO, RiskProfile>()
            .ForMember(dest => dest.RiskName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RiskName));
        CreateMap<RiskProfileUpdateDTO, RiskProfile>()
            .ForMember(dest => dest.RiskName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RiskName));
		CreateMap<RiskProfileCreateDTO, RiskProfile>()
            .ForMember(dest => dest.RiskName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RiskName));	

        CreateMap<BusinessProcessDTO, BusinessProcess>().ReverseMap();
		CreateMap<BusinessProcessCreateDTO, BusinessProcess>().ReverseMap();
        CreateMap<BusinessProcessUpdateDTO, BusinessProcess>();

        CreateMap<RiskDTO, Risk>().ReverseMap();
		CreateMap<RiskCreateDTO, Risk>().ReverseMap();
        CreateMap<RiskUpdateDTO, Risk>();
    }
}