using App.Domain.Vehicles;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class VehicleType : IDomainEntityId
{
    public Guid Id { get; set; }

    public string? VehicleTypeName { get; set; }
    
    public int Size { get; set; }
    
    public string? Make { get; set; }
    
    public string? Model { get; set; }
    
}
