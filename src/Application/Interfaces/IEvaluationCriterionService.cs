using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEvaluationCriterionService
    {
        Task<EvaluationCriterionDto> GetByIdAsync(Guid id);
        Task<List<EvaluationCriterionDto>> GetAllAsync();
        Task<EvaluationCriterionDto> CreateAsync(CreateEvaluationCriterionRequest request);
        Task<EvaluationCriterionDto> UpdateAsync(Guid id, UpdateEvaluationCriterionRequest request);
        Task DeleteAsync(Guid id);
    }
}
