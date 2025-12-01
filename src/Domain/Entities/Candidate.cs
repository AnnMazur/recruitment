using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Candidate : BaseEntity
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? ResumeUrl { get; set; }
        public double TotalRating { get; set; }
        public CandidateStatus Status { get; set; }
        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; } = default!;
        public List<Interview> Interviews { get; set; } = new();
        public List<EvaluationForm> EvaluationForms { get; set; } = new();
    }
}

