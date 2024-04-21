using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class VehicleViolation: IDomainEntityId 
{
    public Guid Id { get; set; }
    
    [MaxLength(128)] 
    public string? Description { get; set; }

}