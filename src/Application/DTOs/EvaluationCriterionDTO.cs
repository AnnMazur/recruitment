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
        public string? Description { get; set; } 
        public double Weight { get; set; } = 1.0;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateEvaluationCriterionRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } 
        public double Weight { get; set; } = 1.0;
    }
    public class UpdateEvaluationCriterionRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Weight { get; set; }
    }
}
