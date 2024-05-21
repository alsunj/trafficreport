using App.Domain.Vehicles;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Violations.Violation, App.DAL.DTO.Violation>().ReverseMap();
        CreateMap<App.Domain.Violations.VehicleViolation, App.DAL.DTO.VehicleViolation>().ReverseMap();
        CreateMap<App.Domain.Evidences.EvidenceType, App.DAL.DTO.EvidenceType>().ReverseMap();
        CreateMap<App.Domain.Evidences.Evidence, App.DAL.DTO.Evidence>().ReverseMap();
        CreateMap<App.Domain.Evidences.Comment, App.DAL.DTO.Comment>().ReverseMap();
        CreateMap<App.Domain.Vehicles.Vehicle, App.DAL.DTO.Vehicle>().ReverseMap();
        CreateMap<App.DTO.v1_0.VehicleModifyDTO, App.DAL.DTO.Vehicle>().ReverseMap();
        CreateMap<App.Domain.Vehicles.VehicleType, App.DAL.DTO.VehicleType>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => (int)src.Size))
            .ReverseMap()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => (EVehicleSize)src.Size));
        CreateMap<App.Domain.Vehicles.AdditionalVehicle, App.DAL.DTO.AdditionalVehicle>().ReverseMap();
    }
    
    
    
}
