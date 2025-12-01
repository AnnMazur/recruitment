using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvaluationCriterionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Type { get; set; } = default!;
        public double Weight { get; set; } = 1.0;
        public int MaxScore { get; set; }
        public string? Options { get; set; } // JSON
        public DateTime CreatedAt { get; set; }
    }

    public class CreateEvaluationCriterionRequest
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public double Weight { get; set; } = 1.0;
        public int MaxScore { get; set; } = 10;
        public string? Options { get; set; }
    }
    public class UpdateEvaluationCriterionRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public double? Weight { get; set; }
        public int? MaxScore { get; set; }
        public string? Options { get; set; }
    }
}
