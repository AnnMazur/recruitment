using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvaluationFormDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int Recommendation { get; set; } // 1–10
        public double TotalScore { get; set; }
        public string? OverallComment { get; set; }
        public DateTime CreatedAt { get; set; }


        public Guid CandidateId { get; set; }
        public string CandidateFullName { get; set; } = default!;
        public Guid InterviewerId { get; set; }
        public string InterviewerName { get; set; } = default!;

        public Guid InterviewId { get; set; }
        public DateTime InterviewScheduledAt { get; set; }

        // Критерии
        public List<CriterionScoreDto> Scores { get; set; } = new();
    }

    public class CreateEvaluationFormRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid InterviewId { get; set; }
        public int Recommendation { get; set; }
        public string? OverallComment { get; set; }
        public List<CreateCriterionScoreRequest> Scores { get; set; } = new();
    }

    public class UpdateEvaluationFormRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Recommendation { get; set; }
        public string? OverallComment { get; set; }
        public List<UpdateCriterionScoreRequest>? Scores { get; set; }
    }
}
