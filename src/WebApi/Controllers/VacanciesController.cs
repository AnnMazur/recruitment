using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VacancyDto>>> GetAll()
        {
            var vacancies = await _vacancyService.GetAllAsync();
            return Ok(vacancies);
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<VacancyDto>>> GetActive()
        {
            var vacancies = await _vacancyService.GetActiveAsync();
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacancyDto>> GetById(Guid id)
        {
            var vacancy = await _vacancyService.GetByIdAsync(id);
            return Ok(vacancy);
        }

        [HttpPost]
        public async Task<ActionResult<VacancyDto>> Create(CreateVacancyRequest request)
        {
            var vacancy = await _vacancyService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = vacancy.Id }, vacancy);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VacancyDto>> Update(Guid id, UpdateVacancyRequest request)
        {
            var vacancy = await _vacancyService.UpdateAsync(id, request);
            return Ok(vacancy);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _vacancyService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/close")]
        public async Task<ActionResult> Close(Guid id)
        {
            await _vacancyService.CloseAsync(id);
            return NoContent();
        }
    }
}