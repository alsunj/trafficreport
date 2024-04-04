using Base.Domain;

namespace App.Domain.Violations;


public class Violation : BaseEntityId
{
    public Guid ViolationTypeId { get; set; }
    public ViolationType? ViolationType { get; set; }

    public string? ViolationName { get; set; }
    public decimal? Severity { get; set; }

    public ICollection<VehicleViolation>? VehicleViolations { get; set; }
}
