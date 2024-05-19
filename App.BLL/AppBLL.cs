using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    
    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }

    private IViolationTypeService? _violationTypes;
    public IViolationTypeService ViolationTypes => _violationTypes ??= new ViolationTypeService(_uow, _uow.ViolationTypeRepository, _mapper);
    
    private IViolationService? _violations;
    public IViolationService Violations => _violations ??= new ViolationService(_uow, _uow.ViolationRepository, _mapper);   

    private IVehicleViolationService? _vehicleViolations;
    public IVehicleViolationService VehicleViolations => _vehicleViolations ??= new VehicleViolationService(_uow, _uow.VehicleViolationRepository, _mapper);   

    private IVehicleTypeService? _vehicleTypes;
    public IVehicleTypeService VehicleTypes => _vehicleTypes ??= new VehicleTypeService(_uow, _uow.VehicleTypeRepository, _mapper);

    private IVehicleService? _vehicles;
    public IVehicleService Vehicles => _vehicles ??= new VehicleService(_uow, _uow.VehicleRepository, _mapper);

    private IAdditionalVehicleService? _additionalVehicles;
    public IAdditionalVehicleService AdditionalVehicles => _additionalVehicles ??= new AdditionalVehicleService(_uow, _uow.AdditionalVehicleRepository, _mapper);

    private IEvidenceTypeService? _evidenceTypes;
    public IEvidenceTypeService EvidenceTypes => _evidenceTypes ??= new EvidenceTypeService(_uow, _uow.EvidenceTypeRepository, _mapper);

    private IEvidenceService? _evidences;
    public IEvidenceService Evidences => _evidences ??= new EvidenceService(_uow, _uow.EvidenceRepository, _mapper);

    private ICommentService? _comments;
    public ICommentService Comments => _comments ??= new CommentService(_uow, _uow.CommentRepository, _mapper);
}
