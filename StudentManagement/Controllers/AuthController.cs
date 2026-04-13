using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Services.Implementations;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Username, dto.Password);

            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { token });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequestDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto.RefreshToken);

            if (result == null)
                return Unauthorized("Invalid refresh token");

            return Ok(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshRequestDto dto)
        {
            var success = await _authService.LogoutAsync(dto.RefreshToken);

            if (!success)
                return BadRequest("Invalid token");

            return Ok("Logged out successfully");
        }
    }
}