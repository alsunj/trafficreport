using App.Contracts.DAL.Repositories;
using App.Domain.Violations;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ViolationTypeRepository : BaseEntityRepository<ViolationType, ViolationType, AppDbContext>,  IViolationTypeRepository
{
    public ViolationTypeRepository(AppDbContext dbContext) :  base(dbContext, new DalDummyMapper<ViolationType, ViolationType>())
    {
    }

}