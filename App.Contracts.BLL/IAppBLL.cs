using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IViolationTypeService ViolationTypes { get; }
    IViolationService Violations { get; }
    IVehicleViolationService VehicleViolations { get; }
}