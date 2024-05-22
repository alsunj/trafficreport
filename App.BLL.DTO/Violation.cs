using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Violation: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public int ViolationType { get; set; }
    
    public string? ViolationName { get; set; }
    
    public decimal? Severity { get; set; }


}