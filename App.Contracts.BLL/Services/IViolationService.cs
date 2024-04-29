using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IViolationService  : IEntityRepository<App.BLL.DTO.Violation>, IViolationRepositoryCustom<App.BLL.DTO.Violation>
{
    
}