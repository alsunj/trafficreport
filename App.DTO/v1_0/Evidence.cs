namespace App.DTO.v1_0;


public class Evidence
{
    public Guid Id { get; set; }

    public Guid EvidenceTypeId{ get; set; }
    
    public Guid VehicleViolationId{ get; set; }
    
    //public Blob Media { get; set; }
    public string? File { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? CreatedAt { get; set; }
}
