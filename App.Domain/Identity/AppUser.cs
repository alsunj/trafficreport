using System.ComponentModel.DataAnnotations;
using App.Domain.Violations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<VehicleViolation>? VehicleViolations { get; set; }
}