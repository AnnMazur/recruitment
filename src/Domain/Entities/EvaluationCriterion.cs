using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class EvaluationCriterion : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Weight { get; set; } = 1.0;
        public List<CriterionScore> Scores { get; set; } = new();

    }
}
