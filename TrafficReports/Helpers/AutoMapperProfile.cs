using AutoMapper;

namespace TrafficReport.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.ViolationType, App.DTO.v1_0.ViolationType>().ReverseMap();
        CreateMap<App.BLL.DTO.Violation, App.DTO.v1_0.Violation>().ReverseMap();
        CreateMap<App.BLL.DTO.VehicleViolation, App.DTO.v1_0.VehicleViolation>().ReverseMap();

    }
}