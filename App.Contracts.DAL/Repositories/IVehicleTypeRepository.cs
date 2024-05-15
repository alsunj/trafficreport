using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IVehicleTypeRepository:  IEntityRepository<DALDTO.VehicleType>, IVehicleTypeRepositoryCustom<DALDTO.VehicleType>
{
}

public interface IVehicleTypeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}