using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;


public interface IEvidenceTypeService : IEntityRepository<App.BLL.DTO.EvidenceType>, IEvidenceTypeRepositoryCustom<App.BLL.DTO.EvidenceType>
{
    
}