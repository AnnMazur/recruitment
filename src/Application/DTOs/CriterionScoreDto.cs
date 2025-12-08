using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CriterionScoreDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; } 
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }


        public Guid EvaluationFormId { get; set; }
        public Guid CriterionId { get; set; }

        public string CriterionName { get; set; } = default!;
        public double CriterionWeight { get; set; }
    }

    public class CreateCriterionScoreRequest
    {
        public Guid CriterionId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateCriterionScoreRequest
    {
        public int? Score { get; set; }
        public string? Comment { get; set; }
    }
}