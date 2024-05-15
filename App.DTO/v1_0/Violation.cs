namespace App.DTO.v1_0;

public class Violation
{
    public Guid Id { get; set; }
    
    public Guid ViolationTypeId { get; set; }
    
    public string? ViolationName { get; set; }
    
    public decimal? Severity { get; set; }


}