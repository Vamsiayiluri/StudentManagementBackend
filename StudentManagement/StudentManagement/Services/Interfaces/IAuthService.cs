using StudentManagement.DTOs;

namespace StudentManagement.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(string username, string password);
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string refreshToken);
    }
}