using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DTO.v1_0;
using AutoMapper;
using Base.BLL;
using ViolationType = App.BLL.DTO.ViolationType;

namespace App.BLL.Services;


public class ViolationTypeService :
    BaseEntityService<App.DAL.DTO.ViolationType, App.BLL.DTO.ViolationType, IViolationTypeRepository>,
    IViolationTypeService
{
    public ViolationTypeService(IAppUnitOfWork uoW, IViolationTypeRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ViolationType, App.BLL.DTO.ViolationType>(mapper))
    {
    }

    public async Task<IEnumerable<ViolationType>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
        
    }
}