using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DTO.v1_0;
using AutoMapper;
using Base.BLL;
using ViolationType = App.BLL.DTO.ViolationType;

namespace App.BLL.Services;


public class ViolationService :
    BaseEntityService<App.DAL.DTO.Violation, App.BLL.DTO.Violation, IViolationRepository>,
    IViolationService
{
    public ViolationService(IAppUnitOfWork uoW, IViolationRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Violation, App.BLL.DTO.Violation>(mapper))
    {
    }

    public async Task<IEnumerable<Violation>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}