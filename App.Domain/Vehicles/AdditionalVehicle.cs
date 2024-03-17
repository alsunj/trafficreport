using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Vehicles;

public class AdditionalVehicle : BaseEntityId
{
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    
    public Guid VehicleViolationId { get; set; }
    public VehicleViolation? VehicleViolation { get; set; }
}
