using static ApiBiblio.DTOs.LivreDTO;

namespace ApiBiblio.Services.Interfaces
{
    public interface ILivreService
    {
        Task<IEnumerable<LivreDto>> GetAllAsync();
        Task<LivreDto> GetByIdAsync(int id);
        Task<LivreDto> CreateAsync(CreateLivreDto createLivreDto);
        Task<LivreDto> UpdateAsync(int id, UpdateLivreDto updateLivreDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<LivreDto>> GetAvailableAsync();
        Task<bool> UpdateAvailabilityAsync(int id, bool disponible);
    }
}
