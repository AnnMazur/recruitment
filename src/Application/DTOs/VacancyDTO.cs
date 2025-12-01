using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VacancyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CandidatesCount { get; set; }
        public int InterviewsCount { get; set; }
        public int ActiveCandidatesCount { get; set; }
    }

    public class CreateVacancyRequest
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class UpdateVacancyRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsClosed { get; set; }
    }

}
