using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IViolationTypeRepository :  IEntityRepository<DALDTO.ViolationType>, IViolationTypeRepositoryCustom<DALDTO.ViolationType>
{
    
}
public interface IViolationTypeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}