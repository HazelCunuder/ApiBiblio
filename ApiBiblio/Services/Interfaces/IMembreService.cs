using ApiBiblio.DTOs;
using static ApiBiblio.DTOs.MembreDTO;

namespace ApiBiblio.Services.Interfaces
{
    public interface IMembreService
    {
        Task<IEnumerable<MembreDto>> GetAllAsync();
        Task<MembreDto> GetByIdAsync(int id);
        Task<MembreDto> CreateAsync(CreateMembreDto createMembreDto);
        Task<MembreDto> UpdateAsync(int id, UpdateMembreDto updateMembreDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EmpruntDTO>> GetHistoriqueAsync(int membreId);
    }
}
