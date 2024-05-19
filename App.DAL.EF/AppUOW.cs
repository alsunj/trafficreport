using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IVehicleViolationRepository? _vehicleViolations;

    public IVehicleViolationRepository VehicleViolationRepository =>
        _vehicleViolations ?? new VehicleViolationRepository(UowDbContext, _mapper);
    
    
    private IViolationTypeRepository? _violationTypes;
    public IViolationTypeRepository ViolationTypeRepository =>
        _violationTypes ?? new ViolationTypeRepository(UowDbContext, _mapper);
    
    private IViolationRepository? _violations;

    public IViolationRepository ViolationRepository =>
        _violations ?? new ViolationRepository(UowDbContext, _mapper);
    private IVehicleTypeRepository? _vehicleTypes;
    public IVehicleTypeRepository VehicleTypeRepository => 
        _vehicleTypes ??= new VehicleTypeRepository(UowDbContext, _mapper);

    private IVehicleRepository? _vehicles;
    public IVehicleRepository VehicleRepository => 
        _vehicles ??= new VehicleRepository(UowDbContext, _mapper);

    private IAdditionalVehicleRepository? _additionalVehicles;
    public IAdditionalVehicleRepository AdditionalVehicleRepository => 
        _additionalVehicles ??= new AdditionalVehicleRepository(UowDbContext, _mapper);

    private IEvidenceTypeRepository? _evidenceTypes;
    public IEvidenceTypeRepository EvidenceTypeRepository => 
        _evidenceTypes ??= new EvidenceTypeRepository(UowDbContext, _mapper);

    private IEvidenceRepository? _evidences;
    public IEvidenceRepository EvidenceRepository => 
        _evidences ??= new EvidenceRepository(UowDbContext, _mapper);

    private ICommentRepository? _comments;
    public ICommentRepository CommentRepository => 
        _comments ??= new CommentRepository(UowDbContext, _mapper);

    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> AppUserRepository => _users ??
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new DalDomainMapper<AppUser, AppUser>(_mapper));
}