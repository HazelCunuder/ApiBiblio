using static ApiBiblio.DTOs.AuthDto;

namespace ApiBiblio.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<string> GenerateJwtTokenAsync(string email, string role);
        Task<bool> ValidateTokenAsync(string token);
    }
}
