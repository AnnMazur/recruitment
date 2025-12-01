using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Interview : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Guid VacancyId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid InterviewerUserId { get; set; }

        public DateTime ScheduledAt { get; set; }
        public bool? Completed { get; set; }

        public Candidate Candidate { get; set; } = default!;
        public Vacancy Vacancy { get; set; } = default!;
        public User CreatedBy { get; set; } = default!;
        public User Interviewer { get; set; } = default!;

        public EvaluationForm? EvaluationForm { get; set; }
    }
}

