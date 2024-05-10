using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class Vehicle : IDomainEntityId
{
    public Guid Id { get; set; }
    
    public Guid VehicleTypeId { get; set; }
    
    public string? Color { get; set; }
    
    public string? RegNr { get; set; }
    
    public decimal? Rating { get; set; }

    
}
