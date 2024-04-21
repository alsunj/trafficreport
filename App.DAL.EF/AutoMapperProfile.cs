using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Violations.ViolationType, App.DAL.DTO.ViolationType>().ReverseMap();
    }
    
}