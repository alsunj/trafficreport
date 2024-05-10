using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class EvidenceType : IDomainEntityId
{
    public Guid Id { get; set; }

    public string? EvidenceTypeName{ get; set; }
    
}
