namespace App.DTO.v1_0;

public class VehicleViolation
{
    public Guid Id { get; set; }
    
    public Guid VehicleId { get; set; }

    public Guid ViolationId { get; set; }

    public Guid AccountId { get; set; }
    
    public string? Description { get; set; }
    
    public string? Coordinates { get; set; }
    
    public string? LocationName { get; set; }

    public DateTime CreatedAt { get; set; }

}