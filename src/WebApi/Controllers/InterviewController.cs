using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewService _interviewService;

        public InterviewsController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InterviewDto>>> GetAll()
        {
            var interviews = await _interviewService.GetAllAsync();
            return Ok(interviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewDto>> GetById(Guid id)
        {
            var interview = await _interviewService.GetByIdAsync(id);
            return Ok(interview);
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<ActionResult<List<InterviewDto>>> GetByCandidate(Guid candidateId)
        {
            var interviews = await _interviewService.GetByCandidateAsync(candidateId);
            return Ok(interviews);
        }

        [HttpGet("vacancy/{vacancyId}")]
        public async Task<ActionResult<List<InterviewDto>>> GetByVacancy(Guid vacancyId)
        {
            var interviews = await _interviewService.GetByVacancyAsync(vacancyId);
            return Ok(interviews);
        }

        [HttpGet("interviewer/{interviewerId}")]
        public async Task<ActionResult<List<InterviewDto>>> GetByInterviewer(Guid interviewerId)
        {
            var interviews = await _interviewService.GetByInterviewerAsync(interviewerId);
            return Ok(interviews);
        }

        [HttpPost]
        public async Task<ActionResult<InterviewDto>> Create(CreateInterviewRequest request)
        {
            var interview = await _interviewService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = interview.Id }, interview);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InterviewDto>> Update(Guid id, UpdateInterviewRequest request)
        {
            var interview = await _interviewService.UpdateAsync(id, request);
            return Ok(interview);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _interviewService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult> Complete(Guid id)
        {
            await _interviewService.CompleteAsync(id);
            return NoContent();
        }
    }
}