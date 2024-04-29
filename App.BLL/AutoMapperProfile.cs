using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.ViolationType, App.BLL.DTO.ViolationType>().ReverseMap();
        CreateMap<App.DAL.DTO.Violation, App.BLL.DTO.Violation>().ReverseMap();
        CreateMap<App.DAL.DTO.VehicleViolation, App.BLL.DTO.VehicleViolation>().ReverseMap();
    }
}