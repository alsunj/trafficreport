using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class EvidenceService :
    BaseEntityService<App.DAL.DTO.Evidence, App.BLL.DTO.Evidence, IEvidenceRepository>,
    IEvidenceService
{
    public EvidenceService(IAppUnitOfWork uoW, IEvidenceRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Evidence, App.BLL.DTO.Evidence>(mapper))
    {
    }

    public async Task<IEnumerable<Evidence>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
    
    public async Task<IEnumerable<Evidence>> GetAllViolationEvidencesSortedAsync(Guid violationId)
    {
        return (await Repository.GetAllViolationEvidencesSortedAsync(violationId))
            .Select(e => Mapper.Map(e));
    }
}