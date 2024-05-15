using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;


public interface IEvidenceRepository:  IEntityRepository<DALDTO.Evidence>, IEvidenceRepositoryCustom<DALDTO.Evidence>
{
}

public interface IEvidenceRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}