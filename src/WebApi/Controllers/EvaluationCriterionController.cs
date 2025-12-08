using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


        public class EvaluationCriterionController : ControllerBase
        {
            private readonly IEvaluationCriterionService _evaluationCriterionService;

            public EvaluationCriterionController(IEvaluationCriterionService evaluationCriterionService)
            {
                 _evaluationCriterionService = evaluationCriterionService;
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<EvaluationCriterionDto>> GetById(Guid id) {
                var evaluationCriterion = await _evaluationCriterionService.GetByIdAsync(id);
                return Ok(evaluationCriterion);
            }


            [HttpGet]
            public async Task<ActionResult<List<EvaluationCriterionDto>>> GetAll()
             {
            var evaluationCriterions = await _evaluationCriterionService.GetAllAsync();
            return Ok(evaluationCriterions);
             }


        [HttpPost]
        public async Task<ActionResult<EvaluationCriterionDto>> Create(CreateEvaluationCriterionRequest request)
        {

            var evaluationCriterion = await _evaluationCriterionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = evaluationCriterion.Id }, evaluationCriterion);
        }
    

        [HttpPut("{id}")]
            public async Task<ActionResult<EvaluationCriterionDto>> Update(Guid id, UpdateEvaluationCriterionRequest request) {
           
            var evaluationCriterion = await _evaluationCriterionService.UpdateAsync(id, request);
            return Ok(evaluationCriterion);

        }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id) {
             await _evaluationCriterionService.DeleteAsync(id);
             return NoContent();

                }
    }
}
