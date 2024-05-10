using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IAdditionalVehicleService : IEntityRepository<App.BLL.DTO.AdditionalVehicle>, IAdditionalVehicleRepositoryCustom<App.BLL.DTO.AdditionalVehicle>
{
    
}