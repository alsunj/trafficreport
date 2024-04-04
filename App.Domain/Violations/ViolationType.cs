using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Violations;

public class ViolationType : BaseEntityId
{
    [MaxLength(128)] 
    public string? ViolationTypeName{ get; set; }
    
    public decimal? Severity { get; set; }
    
    public ICollection<Violation>? Violations { get; set; } = default!;
  
}
