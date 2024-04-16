
using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    //repod
    IVehicleViolationRepository VehicleViolations { get; }
    IViolationTypeRepository ViolationTypes { get; }
    IViolationRepository Violations { get; }
    IEntityRepository<AppUser> Users { get; }


}