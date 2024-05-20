using Base.Domain;

namespace App.Domain.Violations;


public class Violation : BaseEntityId
{
    
    public EViolationType ViolationType { get; set; }

    public string ViolationName { get; set; } = default!;
    public decimal Severity { get; set; }

    public ICollection<VehicleViolation>? VehicleViolations { get; set; }
}
