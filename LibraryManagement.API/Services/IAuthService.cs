using LibraryManagement.API.DTOs;

namespace LibraryManagement.API.Services;

/// <summary>
/// Interface for authentication service operations.
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
