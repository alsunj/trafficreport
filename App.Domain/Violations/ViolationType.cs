using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Violations;

public class ViolationType : BaseEntityId
{
    public string ViolationTypeName { get; set; } = default!;

    public decimal Severity { get; set; } = default!;
    
    public ICollection<Violation>? Violations { get; set; } = default!;
  
}
