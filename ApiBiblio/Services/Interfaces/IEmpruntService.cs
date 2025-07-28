using ApiBiblio.DTOs;

namespace ApiBiblio.Services.Interfaces
{
    public interface IEmpruntService
    {
        Task<IEnumerable<EmpruntDTO>> GetAllAsync();
        Task<EmpruntDTO> GetByIdAsync(int id);
        Task<EmpruntDTO> CreateAsync(CreateEmpruntDto createEmpruntDto, int employeId);
        Task<bool> RetournerLivresAsync(int empruntId, int employeId);
        Task<IEnumerable<EmpruntDTO>> GetEmpruntsByMembreAsync(int membreId);
        Task<IEnumerable<EmpruntDTO>> GetEmpruntsEnCoursAsync();
    }
}
