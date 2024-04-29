using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL: BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    
    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }

    private IViolationTypeService? _violationTypes;
    public IViolationTypeService ViolationTypes => _violationTypes?? new ViolationTypeService(_uow, _uow.ViolationTypes, _mapper);
    
    private IViolationService? _violations;
    public IViolationService Violations => _violations?? new ViolationService(_uow, _uow.Violations, _mapper);   

    public IVehicleViolationService? _vehicleViolations;
    public IVehicleViolationService VehicleViolations => _vehicleViolations?? new VehicleViolationService(_uow, _uow.VehicleViolations, _mapper);   





}