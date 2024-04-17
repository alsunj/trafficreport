using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IVehicleViolationRepository? _vehicleViolations;

    public IVehicleViolationRepository VehicleViolations =>
        _vehicleViolations ?? new VehicleViolationRepository(UowDbContext);
    
    
    private IViolationTypeRepository? _violationTypes;
    public IViolationTypeRepository ViolationTypes =>
        _violationTypes ?? new ViolationTypeRepository(UowDbContext);
    
    private IViolationRepository? _violations;

    public IViolationRepository Violations =>
        _violations ?? new ViolationRepository(UowDbContext);

    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new DalDummyMapper<AppUser, AppUser>());
}