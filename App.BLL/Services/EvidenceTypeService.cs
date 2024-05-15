using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class EvidenceTypeService :
    BaseEntityService<App.DAL.DTO.EvidenceType, App.BLL.DTO.EvidenceType, IEvidenceTypeRepository>,
    IEvidenceTypeService
{
    public EvidenceTypeService(IAppUnitOfWork uoW, IEvidenceTypeRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.EvidenceType, App.BLL.DTO.EvidenceType>(mapper))
    {
    }

    public async Task<IEnumerable<EvidenceType>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}