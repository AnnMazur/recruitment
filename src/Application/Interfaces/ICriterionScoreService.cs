using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICriterionScoreService
    {
        Task<CriterionScoreDto> GetByIdAsync(Guid id);
        Task<List<CriterionScoreDto>> GetByEvaluationFormAsync(Guid evaluationFormId);
        Task<List<CriterionScoreDto>> GetByCriterionAsync(Guid criterionId);
        Task<CriterionScoreDto> CreateAsync(CreateCriterionScoreRequest request);
        Task<CriterionScoreDto> UpdateAsync(Guid id, UpdateCriterionScoreRequest request);
        Task DeleteAsync(Guid id);
        Task<List<CriterionScoreDto>> GetByCandidateAsync(Guid candidateId);
        

    }
}
