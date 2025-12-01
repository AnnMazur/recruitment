using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vacancy : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsClosed { get; set; } = false;
        public DateTime? ClosedAt { get; set; }

        public List<Candidate> Candidates { get; set; } = new();
        public List<Interview> Interviews { get; set; } = new();
    }
}

