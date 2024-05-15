using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IEvidenceService : IEntityRepository<App.BLL.DTO.Evidence>, IEvidenceRepositoryCustom<App.BLL.DTO.Evidence>
{
    
}