using App.Contracts.DAL.Repositories;
using App.Domain.Violations;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;


public class ViolationRepository : BaseEntityRepository<Violation, Violation, AppDbContext>,  IViolationRepository
{
    public ViolationRepository(AppDbContext dbContext) :  base(dbContext, new DalDummyMapper<Violation, Violation>())
    {

    }
    public async Task<IEnumerable<Violation>> GetAllWithViolationTypesAsync()
    {
        return await RepoDbSet.Include(v => v.ViolationType).ToListAsync();
    }
}