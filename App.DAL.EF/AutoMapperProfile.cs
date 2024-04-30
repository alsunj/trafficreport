using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Violations.ViolationType, App.DAL.DTO.ViolationType>().ReverseMap();
        CreateMap<App.Domain.Violations.Violation, App.DAL.DTO.Violation>().ReverseMap();
        CreateMap<App.Domain.Violations.VehicleViolation, App.DAL.DTO.VehicleViolation>().ReverseMap();


    }
    
}