using Base.Contracts.Domain;


namespace App.BLL.DTO;

public class Evidence : IDomainEntityId
{
    public Guid Id { get; set; }

    public Guid EvidenceTypeId{ get; set; }
    
    public Guid VehicleViolationId{ get; set; }
    
    public string? File { get; set; }

    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
