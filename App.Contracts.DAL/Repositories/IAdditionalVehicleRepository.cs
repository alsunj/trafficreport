using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;
using DALDTO = App.DAL.DTO;

public interface IAdditionalVehicleRepository:  IEntityRepository<DALDTO.AdditionalVehicle>, IAdditionalVehicleRepositoryCustom<DALDTO.AdditionalVehicle>
{
}

public interface IAdditionalVehicleRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    Task<IEnumerable<TEntity>> GetAllByVehicleViolationSortedAsync(Guid vehicleViolationId);
}
