
using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    //repod
    IVehicleViolationRepository VehicleViolationRepository { get; }
    IViolationRepository ViolationRepository { get; }
    IEntityRepository<AppUser> AppUserRepository { get; }
    IVehicleTypeRepository VehicleTypeRepository { get; }
    IVehicleRepository VehicleRepository { get; }
    IAdditionalVehicleRepository AdditionalVehicleRepository { get; }
    IEvidenceTypeRepository EvidenceTypeRepository { get; }
    IEvidenceRepository EvidenceRepository { get; }
    ICommentRepository CommentRepository { get; }


}
