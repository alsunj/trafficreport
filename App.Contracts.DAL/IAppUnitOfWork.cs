
using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    //repod
    IVehicleViolationRepository VehicleViolationRepository { get; }
    
    IEntityRepository<AppUser> Users { get; }

}