using AutoMapper;

namespace TrafficReport.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.ViolationType, App.DTO.v1_0.ViolationType>().ReverseMap();
        CreateMap<App.BLL.DTO.Violation, App.DTO.v1_0.Violation>().ReverseMap();
        CreateMap<App.BLL.DTO.EvidenceType, App.DTO.v1_0.EvidenceType>().ReverseMap();
        CreateMap<App.BLL.DTO.Evidence, App.DTO.v1_0.Evidence>().ReverseMap();
        CreateMap<App.BLL.DTO.Comment, App.DTO.v1_0.Comment>().ReverseMap();
        CreateMap<App.BLL.DTO.Vehicle, App.DTO.v1_0.Vehicle>().ReverseMap();
        CreateMap<App.BLL.DTO.VehicleType, App.DTO.v1_0.VehicleType>().ReverseMap();
        CreateMap<App.BLL.DTO.AdditionalVehicle, App.DTO.v1_0.AdditionalVehicle>().ReverseMap();
        CreateMap<App.BLL.DTO.VehicleViolation, App.DTO.v1_0.VehicleViolation>().ReverseMap();

    }
}