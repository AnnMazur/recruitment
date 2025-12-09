
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CandidateDto>>> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDto>> GetById(Guid id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            return Ok(candidate);
        }

        [HttpGet("vacancy/{vacancyId}")]
        public async Task<ActionResult<List<CandidateDto>>> GetByVacancy(Guid vacancyId) { 
            var candidate = await _candidateService.GetByVacancyAsync(vacancyId);   
            return Ok(candidate);
        
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDto>> Create(CreateCandidateRequest request)
        {
            var candidate = await _candidateService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CandidateDto>> Update(Guid id, UpdateCandidateRequest request)
        {
            var candidate = await _candidateService.UpdateAsync(id, request);
            return Ok(candidate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _candidateService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{candidateId}/update-rating")]
        public async Task<ActionResult> UpdateCandidateRating(Guid candidateId)
        {
            await _candidateService.UpdateCandidateRatingAsync(candidateId);
            return NoContent();
        }


    }

}
