using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CriterionScore : BaseEntity
    {
        public Guid EvaluationFormId { get; set; }
        public Guid CriterionId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }

        public EvaluationForm EvaluationForm { get; set; } = default!;
        public EvaluationCriterion Criterion { get; set; } = default!;
    }
}