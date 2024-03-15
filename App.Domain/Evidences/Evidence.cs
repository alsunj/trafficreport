using System.ComponentModel.DataAnnotations;
using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Evidences;

public class Evidence : BaseEntityId
{
    public Guid EvidenceTypeId{ get; set; }
    public EvidenceType? EvidenceType{ get; set; }
    
    public Guid VehicleViolationId{ get; set; }
    public VehicleViolation? VehicleViolation{ get; set; }
    
    //public Blob Media { get; set; }
    
    [MaxLength(256)] 
    public string? Description { get; set; }
    
    public DateTime CreatedAt = DateTime.Now;
}
