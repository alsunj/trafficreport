using App.Domain.Vehicles;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Violation: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public EVehicleSize Size { get; set; }
    
    public string? ViolationName { get; set; }
    
    public decimal? Severity { get; set; }


}