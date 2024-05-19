using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IVehicleRepository:  IEntityRepository<DALDTO.Vehicle>, IVehicleRepositoryCustom<DALDTO.Vehicle>
{
}

public interface IVehicleRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    Task<TEntity> GetByLicensePlateAsync(string licensePlate);
}