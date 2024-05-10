using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class VehicleTypeService :
    BaseEntityService<App.DAL.DTO.VehicleType, App.BLL.DTO.VehicleType, IVehicleTypeRepository>,
    IVehicleTypeService
{
    public VehicleTypeService(IAppUnitOfWork uoW, IVehicleTypeRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.VehicleType, App.BLL.DTO.VehicleType>(mapper))
    {
    }

    public async Task<IEnumerable<VehicleType>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}