using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
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


        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationFormDto>> GetById(Guid id) {

            var evaluationForm = await _evaluationFormService.GetByIdAsync(id);
            return Ok(evaluationForm);
        }

        [HttpGet]
        public async Task<ActionResult<List<EvaluationFormDto?>>> GetAll() {
            var evaluationForms = await _evaluationFormService.GetAllAsync();
            return Ok(evaluationForms);
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByCandidate(Guid candidateId) {
            var evaluationForm = await _evaluationFormService.GetByCandidateAsync(candidateId);
            return Ok(evaluationForm);
        }

        [HttpGet("interviewer/{interviewerId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByInterviewer(Guid interviewerId)
        {
            var evaluationForm = await _evaluationFormService.GetByInterviewerAsync(interviewerId);
            return Ok(evaluationForm);
        }

        [HttpGet("interview/{interviewId}")]
        public async Task<ActionResult<List<EvaluationFormDto>>> GetByInterview(Guid interviewId)
        {
            var evaluationForm = await _evaluationFormService.GetByInterviewAsync(interviewId);
            return Ok(evaluationForm);
        }
        [HttpPost]
        public async Task<ActionResult<EvaluationFormDto>> Create(CreateEvaluationFormRequest request)
        {
            var evaluationForm = await _evaluationFormService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = evaluationForm.Id }, evaluationForm);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EvaluationFormDto>> Update(Guid id, UpdateEvaluationFormRequest request)
        {
            var evaluationForm = await _evaluationFormService.UpdateAsync(id, request);
            return Ok(evaluationForm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _evaluationFormService.DeleteAsync(id);
            return NoContent();
        }
    }
}
