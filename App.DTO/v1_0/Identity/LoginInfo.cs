using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0.Identity;

public class LoginInfo
{
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Email { get; set; } = default!;
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Password { get; set; } = default!;
}
