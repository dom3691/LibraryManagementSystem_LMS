namespace LibraryManagement.API.DTOs;

/// <summary>
/// Data Transfer Object for authentication response containing JWT token.
/// </summary>
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string Username { get; set; } = string.Empty;
}
