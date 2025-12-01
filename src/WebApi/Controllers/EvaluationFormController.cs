using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationFormController : ControllerBase
    {
        private readonly IEvaluationFormService _evaluationFormService;

        public EvaluationFormController(IEvaluationFormService evaluationFormService)
        {
            _evaluationFormService = evaluationFormService;
        }


       /* [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationFormDto>> GetById(Guid id) { 
        }

        [HttpGet]
        public async Task<ActionResult<List<EvaluationFormDto?>> GetAll() { 
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByCandidate(Guid candidateId) { 
        }

        [HttpGet("interviewer/{interviewerId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByInterviewer(Guid interviewerId)
        {

        }

        [HttpGet("interview/{interviewId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByInterview(Guid interviewId)
        {

        }
        [HttpPost]
        public async Task<ActionResult<EvaluationFormDto>> Create(CreateEvaluationFormRequest request)
        {

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EvaluationFormDto>> Update(Guid id, UpdateEvaluationFormRequest request)
        {

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

        }
   */ }
}
