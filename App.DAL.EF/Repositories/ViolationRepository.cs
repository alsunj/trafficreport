using App.Contracts.DAL.Repositories;
using AutoMapper;
using DALDTO = App.DAL.DTO;
using APPDomain = App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;


public class ViolationRepository : BaseEntityRepository<APPDomain.Violations.Violation, DALDTO.Violation, AppDbContext>,  IViolationRepository
{
    public ViolationRepository(AppDbContext dbContext, IMapper mapper) :  base(dbContext, new DalDomainMapper<APPDomain.Violations.Violation, DALDTO.Violation>(mapper))
    {

    }
    public async Task<IEnumerable<DALDTO.Violation>> GetAllWithViolationTypesAsync()
    {
        throw new NotImplementedException();

        //return await RepoDbSet.Include(v => v.ViolationType).ToListAsync();
    }
}