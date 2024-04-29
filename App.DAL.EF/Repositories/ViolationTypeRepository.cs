using App.Contracts.DAL.Repositories;
using AutoMapper;
using DALDTO = App.DAL.DTO;

using APPDomain = App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ViolationTypeRepository : BaseEntityRepository<APPDomain.Violations.ViolationType, DALDTO.ViolationType, AppDbContext>,  IViolationTypeRepository
{
    public ViolationTypeRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.ViolationType, DALDTO.ViolationType>(mapper))
    {
    }
    public async Task<IEnumerable<DALDTO.ViolationType>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        var res = await query.ToListAsync();
//        query = query.OrderBy(c => c.ViolationType);
        
        return res.Select(e => Mapper.Map(e));
    }
}