using App.Contracts.DAL.Repositories;
using App.Domain.Violations;
using AutoMapper;
using Base.DAL.EF;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;


public class ViolationRepository : BaseEntityRepository<APPDomain.Violations.Violation, DALDTO.Violation, AppDbContext>,  IViolationRepository
{
    public ViolationRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.Violation, DALDTO.Violation>(mapper))
    {

    }
    public async Task<IEnumerable<DALDTO.Violation>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        var res = await query.ToListAsync(); 
        query = query.OrderBy(c => c.Severity);
        
        return (await query.ToListAsync()).Select(e => Mapper.Map(e));
    }
  
}