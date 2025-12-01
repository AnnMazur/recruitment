using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateDto> GetByIdAsync(Guid id);
        Task<List<CandidateDto>> GetAllAsync();
        Task<List<CandidateDto>> GetByVacancyAsync(Guid vacancyId);
        Task<CandidateDto> CreateAsync(CreateCandidateRequest request);
        Task<CandidateDto> UpdateAsync(Guid id, UpdateCandidateRequest request);
        Task DeleteAsync(Guid id);
        Task UpdateCandidateRatingAsync(Guid candidateId);

    }
}