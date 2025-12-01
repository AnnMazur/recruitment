using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEvaluationFormService
    {
        Task<EvaluationFormDto> GetByIdAsync(Guid id);
        Task<List<EvaluationFormDto>> GetAllAsync();
        Task<List<EvaluationFormDto>> GetByCandidateAsync(Guid candidateId);
        Task<List<EvaluationFormDto>> GetByInterviewerAsync(Guid interviewerId);
        Task<List<EvaluationFormDto>> GetByInterviewAsync(Guid interviewId);

        Task<EvaluationFormDto> CreateAsync(CreateEvaluationFormRequest request);
        Task<EvaluationFormDto> UpdateAsync(Guid id, UpdateEvaluationFormRequest request);
        Task DeleteAsync(Guid id);       

    }
}
