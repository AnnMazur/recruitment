using Application.DTOs;

namespace Application.Interfaces
{
    public interface IVacancyService
    {
        Task<VacancyDto> GetByIdAsync(Guid id);
        Task<List<VacancyDto>> GetAllAsync();
        Task<List<VacancyDto>> GetActiveAsync();
        Task<VacancyDto> CreateAsync(CreateVacancyRequest request);
        Task<VacancyDto> UpdateAsync(Guid id, UpdateVacancyRequest request);
        Task DeleteAsync(Guid id);
        Task CloseAsync(Guid id);
    }
}
