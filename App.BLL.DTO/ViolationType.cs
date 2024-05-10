using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class ViolationType: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string? ViolationTypeName{ get; set; }
    
    public decimal? Severity { get; set; }

}