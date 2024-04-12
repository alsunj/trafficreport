using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IVehicleViolationRepository? _vehicleViolationRepository;

    public IVehicleViolationRepository VehicleViolationRepository =>
        _vehicleViolationRepository ?? new VehicleViolationRepository(UowDbContext);
}