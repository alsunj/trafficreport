using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IVehicleViolationRepository :  IEntityRepository<DALDTO.VehicleViolation>, IVehicleViolationRepositoryCustom<DALDTO.VehicleViolation>
{
    
}

public interface IVehicleViolationRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}