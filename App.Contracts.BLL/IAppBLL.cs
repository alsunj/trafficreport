using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IViolationTypeService ViolationTypes { get; }
    IViolationService Violations { get; }
    IVehicleViolationService VehicleViolations { get; }
    
    IVehicleTypeService VehicleTypes { get; }
    IVehicleService Vehicles{ get; }
    
    IAdditionalVehicleService AdditionalVehicles { get; }
    IEvidenceTypeService EvidenceTypes { get; }

    IEvidenceService Evidences{ get; }

    ICommentService Comments { get; }

    
    
}