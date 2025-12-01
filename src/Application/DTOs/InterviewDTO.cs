using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public bool? Completed { get; set; }
        public DateTime CreatedAt { get; set; }


        public Guid CandidateId { get; set; }
        public string CandidateFullName { get; set; } = default!;

        public Guid VacancyId { get; set; }
        public string VacancyTitle { get; set; } = default!;

        public Guid CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = default!;

        public Guid InterviewerUserId { get; set; }
        public string InterviewerUserName { get; set; } = default!;

        public Guid? EvaluationFormId { get; set; }
        public bool HasEvaluationForm { get; set; }
    }

    public class CreateInterviewRequest
    {
        public Guid CandidateId { get; set; }
        public Guid VacancyId { get; set; }
        public Guid InterviewerUserId { get; set; }
        public DateTime ScheduledAt { get; set; }
    }

    public class UpdateInterviewRequest
    {
        public Guid? InterviewerUserId { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public bool? Completed { get; set; }
    }

    public class InterviewWithDetailsDto
    {
        public InterviewDto Interview { get; set; } = default!;
        public EvaluationFormDto? EvaluationForm { get; set; }
    }
}
