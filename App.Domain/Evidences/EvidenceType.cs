using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Evidences;

public class EvidenceType : BaseEntityId
{
    [MaxLength(128)] 
    public string? EvidenceTypeName{ get; set; }
    
    public ICollection<Evidence>? Evidences { get; set; }
}
