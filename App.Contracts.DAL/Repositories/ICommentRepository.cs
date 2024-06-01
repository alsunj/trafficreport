using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;


public interface ICommentRepository:  IEntityRepository<DALDTO.Comment>, ICommentRepositoryCustom<DALDTO.Comment>
{
}

public interface ICommentRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    Task<IEnumerable<TEntity>> GetAllViolationCommentsSortedAsync(Guid violationId);

    Task<IEnumerable<TEntity>> GetAllViolationCommentsWithParentCommentAsync(Guid parentCommentId);

    Task<IEnumerable<TEntity>> GetAllViolationCommentsWithNoParentCommentAsync(Guid vehicleViolationId);


}