using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.DTOs;

/// <summary>
/// Data Transfer Object for user login.
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "Username or Email is required")]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
