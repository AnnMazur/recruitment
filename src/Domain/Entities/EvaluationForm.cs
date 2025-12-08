using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluationForm : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int Recommendation { get; set; } // 1–10
        public double TotalScore { get; set; }
        public string? OverallComment { get; set; }

        public Guid CandidateId { get; set; }
        public Guid InterviewerId { get; set; }
        public Guid InterviewId { get; set; }

        public Candidate Candidate { get; set; } = default!;
        public User Interviewer { get; set; } = default!;
        public Interview Interview { get; set; } = default!;
        public List<CriterionScore> Scores { get; set; } = new();
    }
}

