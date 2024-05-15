using Base.Contracts.Domain;


namespace App.BLL.DTO;

public class AdditionalVehicle : IDomainEntityId
{
    public Guid Id { get; set; }
    
    public Guid VehicleId { get; set; }
    
    public Guid VehicleViolationId { get; set; }
}
