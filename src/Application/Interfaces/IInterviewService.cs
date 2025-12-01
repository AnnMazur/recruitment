using Application.DTOs;

namespace Application.Interfaces
{
    public interface IInterviewService
    {
        Task<InterviewDto> GetByIdAsync(Guid id);
        Task<List<InterviewDto>> GetAllAsync();
        Task<List<InterviewDto>> GetByCandidateAsync(Guid candidateId);
        Task<List<InterviewDto>> GetByVacancyAsync(Guid vacancyId);
        Task<List<InterviewDto>> GetByInterviewerAsync(Guid interviewerId);
        Task<InterviewDto> CreateAsync(CreateInterviewRequest request);
        Task<InterviewDto> UpdateAsync(Guid id, UpdateInterviewRequest request);
        Task DeleteAsync(Guid id);
        Task CompleteAsync(Guid id);
    }
}
