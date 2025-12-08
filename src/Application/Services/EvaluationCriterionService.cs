using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EvaluationCriterionService : IEvaluationCriterionService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EvaluationCriterionService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EvaluationCriterionDto> GetByIdAsync(Guid id)
        {
            var evaluationCriterion = await _context.EvaluationCriteria
               .FirstOrDefaultAsync(ec => ec.Id == id);

            if (evaluationCriterion == null)
                throw new KeyNotFoundException($"Criteria with ID {id} not found");

            return _mapper.Map<EvaluationCriterionDto>(evaluationCriterion);

        }
        public async Task<List<EvaluationCriterionDto>> GetAllAsync()
        {
            var evaluationCriterions = await _context.EvaluationCriteria
              .OrderByDescending(ec => ec.Name)
              .ToListAsync();

            return _mapper.Map<List<EvaluationCriterionDto>>(evaluationCriterions);

        }

        public async Task<EvaluationCriterionDto> CreateAsync(CreateEvaluationCriterionRequest request)
        {
            var criterionExists = await _context.EvaluationCriteria.AnyAsync(ec => ec.Name == request.Name);
            if (criterionExists) 
                throw new ArgumentException($"Criterion with name {request.Name} already exists");

            var evaluationCriterion = _mapper.Map<EvaluationCriterion>(request);

            _context.EvaluationCriteria.Add(evaluationCriterion);
            await _context.SaveChangesAsync();

            return _mapper.Map<EvaluationCriterionDto>(evaluationCriterion);


        }
        public async Task<EvaluationCriterionDto> UpdateAsync(Guid id, UpdateEvaluationCriterionRequest request) {

            var evaluationCriterion = await _context.EvaluationCriteria.FindAsync(id);

            if (evaluationCriterion == null)
                throw new KeyNotFoundException("Criterion with this id not found");

            _mapper.Map(request, evaluationCriterion);
            await _context.SaveChangesAsync();
            return _mapper.Map<EvaluationCriterionDto>(evaluationCriterion);

        } 
        public async Task DeleteAsync(Guid id) {

            var evaluationCriterion = await _context.EvaluationCriteria.FindAsync(id);

            if (evaluationCriterion == null)
                throw new KeyNotFoundException("Criterion with this id not found");

            _context.EvaluationCriteria.Remove(evaluationCriterion);
            await _context.SaveChangesAsync();

        } 
    }
}
