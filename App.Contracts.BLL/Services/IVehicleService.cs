using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IVehicleService : IEntityRepository<App.BLL.DTO.Vehicle>, IVehicleRepositoryCustom<App.BLL.DTO.Vehicle>
{
    
}