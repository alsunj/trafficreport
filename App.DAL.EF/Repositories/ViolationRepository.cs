using App.Contracts.DAL.Repositories;
using App.Domain.Violations;

namespace App.DAL.EF.Repositories;


public class ViolationRepository : BaseEntityRepository<Violation, Violation, AppDbContext>,  IViolationRepository
{
    public ViolationRepository(AppDbContext dbContext) :  base(dbContext, new DalDummyMapper<Violation, Violation>())
    {
        
    }
}