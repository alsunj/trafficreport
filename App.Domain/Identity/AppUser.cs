using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    [MaxLength(128)]
    public string Username { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ClosedAt { get; set; }
    
}