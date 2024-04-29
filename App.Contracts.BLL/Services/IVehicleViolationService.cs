using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IVehicleViolationService : IEntityRepository<App.BLL.DTO.VehicleViolation>, IVehicleViolationRepositoryCustom<App.BLL.DTO.VehicleViolation>
{
    
}