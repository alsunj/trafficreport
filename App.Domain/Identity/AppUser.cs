using System.ComponentModel.DataAnnotations;
using App.Domain.Violations;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<VehicleViolation>? VehicleViolations { get; set; }
}