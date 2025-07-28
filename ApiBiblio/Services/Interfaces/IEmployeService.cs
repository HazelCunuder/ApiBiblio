using static ApiBiblio.DTOs.AuthDto;

namespace ApiBiblio.Services.Interfaces
{
    public interface IEmployeService
    {
        Task<bool> CreateAsync(CreateEmployeDto createEmployeDto);
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task<string> GetRoleAsync(string email);
        Task<int> GetEmployeIdAsync(string email);
    }
}
