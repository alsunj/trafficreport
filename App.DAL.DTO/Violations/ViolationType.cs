using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class ViolationType: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string? ViolationTypeName{ get; set; }
    
    public decimal? Severity { get; set; }
}