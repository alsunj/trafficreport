using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class AdditionalVehicleService :
    BaseEntityService<App.DAL.DTO.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle, IAdditionalVehicleRepository>,
    IAdditionalVehicleService
{
    public AdditionalVehicleService(IAppUnitOfWork uoW, IAdditionalVehicleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle>(mapper))
    {
    }

    public async Task<IEnumerable<AdditionalVehicle>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}