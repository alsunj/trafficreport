using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;


public interface IEvidenceTypeRepository:  IEntityRepository<DALDTO.EvidenceType>, IEvidenceTypeRepositoryCustom<DALDTO.EvidenceType>
{
}

public interface IEvidenceTypeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}