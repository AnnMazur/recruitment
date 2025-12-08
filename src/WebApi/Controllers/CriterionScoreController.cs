using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriterionScoresController : ControllerBase
    {
        private readonly ICriterionScoreService _criterionScoreService;

        public CriterionScoresController(ICriterionScoreService criterionScoreService)
        {
            _criterionScoreService = criterionScoreService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CriterionScoreDto>> GetById(Guid id)
        {
            var score = await _criterionScoreService.GetByIdAsync(id);
            return Ok(score);
        }

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<List<CriterionScoreDto>>> GetByEvaluationForm(Guid formId)
        {
            var scores = await _criterionScoreService.GetByEvaluationFormAsync(formId);
            return Ok(scores);
        }

        [HttpGet("criterion/{criterionId}")]
        public async Task<ActionResult<List<CriterionScoreDto>>> GetByCriterion(Guid criterionId)
        {
            var scores = await _criterionScoreService.GetByCriterionAsync(criterionId);
            return Ok(scores);
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<ActionResult<List<CriterionScoreDto>>> GetByCandidate(Guid candidateId)
        {
            var scores = await _criterionScoreService.GetByCandidateAsync(candidateId);
            return Ok(scores);
        }


        [HttpPost]
        public async Task<ActionResult<CriterionScoreDto>> Create(CreateCriterionScoreRequest request)
        {
            var score = await _criterionScoreService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = score.Id }, score);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CriterionScoreDto>> Update(Guid id, UpdateCriterionScoreRequest request)
        {
            var score = await _criterionScoreService.UpdateAsync(id, request);
            return Ok(score);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _criterionScoreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
