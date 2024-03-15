using System.ComponentModel.DataAnnotations;
using App.Domain.Evidences;
using Base.Domain;

namespace App.Domain.Vehicles;

public class EvidenceType : BaseEntityId
{
    [MaxLength(128)] 
    public string? EvidenceTypeName{ get; set; }
    
    public ICollection<Evidence>? Evidences { get; set; }
}
