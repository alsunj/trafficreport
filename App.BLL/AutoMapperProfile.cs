using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.ViolationType, App.BLL.DTO.ViolationType>().ReverseMap();
        CreateMap<App.DAL.DTO.Violation, App.BLL.DTO.Violation>().ReverseMap();
        CreateMap<App.DAL.DTO.VehicleViolation, App.BLL.DTO.VehicleViolation>().ReverseMap();
        CreateMap<App.DAL.DTO.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle>().ReverseMap();
        CreateMap<App.DAL.DTO.VehicleType, App.BLL.DTO.VehicleType>().ReverseMap();
        CreateMap<App.DAL.DTO.Vehicle, App.BLL.DTO.Vehicle>().ReverseMap();
        CreateMap<App.DAL.DTO.Evidence, App.BLL.DTO.Evidence>().ReverseMap();
        CreateMap<App.DAL.DTO.EvidenceType, App.BLL.DTO.EvidenceType>().ReverseMap();
        CreateMap<App.DAL.DTO.Comment, App.BLL.DTO.Comment>().ReverseMap();
    }
}