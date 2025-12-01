using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CandidateDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? ResumeUrl { get; set; }
        public double TotalRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid VacancyId { get; set; }
        public string Status { get; set; } = "OnReview"; 
    }

    public class CreateCandidateRequest
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public Guid VacancyId { get; set; }
        public string? ResumeUrl { get; set; }
        public string Status { get; set; } = "OnReview";
    }

    public class UpdateCandidateRequest
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? ResumeUrl { get; set; }
    }

    public class CandidateInterviewsDto
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public double TotalRating { get; set; }
        public List<InterviewDto> Interviews { get; set; } = new();
    }

    public class CandidateEvaluationsDto
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public double TotalRating { get; set; }
        public List<CriterionScoreDto> Evaluations { get; set; } = new();
    }
}
