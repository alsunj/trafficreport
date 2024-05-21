using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class VehicleViolationService :
    BaseEntityService<App.DAL.DTO.VehicleViolation, App.BLL.DTO.VehicleViolation, IVehicleViolationRepository>,
    IVehicleViolationService
{
    
    public VehicleViolationService(IAppUnitOfWork uoW, IVehicleViolationRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.VehicleViolation, App.BLL.DTO.VehicleViolation>(mapper))
    {
        
    }

    public async Task<IEnumerable<VehicleViolation>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
    
    public async Task<IEnumerable<VehicleViolation>> GetAllUserVehicleViolationsSortedAsync(Guid userId)
    {
        return (await Repository.GetAllUserVehicleViolationsSortedAsync(userId))
            .Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<VehicleViolation>> GetAllVehicleViolationsByVehicleId(Guid vehicleId)
    {
        return (await Repository.GetAllVehicleViolationsByVehicleId(vehicleId))
            .Select(e => Mapper.Map(e));
    }


    public async Task<IEnumerable<VehicleViolation>> GetAllVehicleViolationsByLicensePlateSortedAsync(string licensePlate)
    {
        return (await Repository.GetAllVehicleViolationsByLicensePlateSortedAsync(licensePlate))
            .Select(e => Mapper.Map(e));
    }
    
}