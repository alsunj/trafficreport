using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IViolationTypeService : IEntityRepository<App.BLL.DTO.ViolationType>, IViolationTypeRepositoryCustom<App.BLL.DTO.ViolationType>
{
    
}