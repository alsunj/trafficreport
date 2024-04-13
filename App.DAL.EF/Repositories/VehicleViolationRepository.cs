using App.Contracts.DAL.Repositories;
using App.Domain.Violations;
using Base.Contracts.DAL;

namespace App.DAL.EF.Repositories;

public class VehicleViolationRepository : BaseEntityRepository<VehicleViolation, VehicleViolation, AppDbContext>,  IVehicleViolationRepository
{
    public VehicleViolationRepository(AppDbContext dbContext) :  base(dbContext, new DalDummyMapper<VehicleViolation, VehicleViolation>())
    {
    }
}