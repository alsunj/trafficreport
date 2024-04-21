using DALDTO = App.DAL.DTO;

using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IViolationRepository :  IEntityRepository<DALDTO.Violation>
{
    public Task<IEnumerable<DALDTO.Violation>> GetAllWithViolationTypesAsync();
}