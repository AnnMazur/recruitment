using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CriterionScoreService : ICriterionScoreService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CriterionScoreService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CriterionScoreDto> GetByIdAsync(Guid id)
        {
            var score = await _context.CriterionScores
                .Include(cs => cs.Criterion)
                .Include(cs => cs.EvaluationForm)
                    .ThenInclude(ef => ef.Candidate)
                .FirstOrDefaultAsync(cs => cs.Id == id);

            if (score == null)
                throw new KeyNotFoundException($"Criterion score with ID {id} not found");

            return _mapper.Map<CriterionScoreDto>(score);
        }

        public async Task<List<CriterionScoreDto>> GetByEvaluationFormAsync(Guid evaluationFormId)
        {
            var formExists = await _context.EvaluationForms.AnyAsync(ef => ef.Id == evaluationFormId);
            if (!formExists)
                throw new ArgumentException($"Evaluation form with ID {evaluationFormId} not found");

            var scores = await _context.CriterionScores
                .Include(cs => cs.Criterion)
                .Where(cs => cs.EvaluationFormId == evaluationFormId)
                .OrderBy(cs => cs.Criterion.Name)
                .ToListAsync();

            return _mapper.Map<List<CriterionScoreDto>>(scores);
        }

        public async Task<List<CriterionScoreDto>> GetByCriterionAsync(Guid criterionId)
        {
            var criterionExists = await _context.EvaluationCriteria.AnyAsync(ec => ec.Id == criterionId);
            if (!criterionExists)
                throw new ArgumentException($"Criterion with ID {criterionId} not found");

            var scores = await _context.CriterionScores
                .Include(cs => cs.Criterion)
                .Include(cs => cs.EvaluationForm)
                    .ThenInclude(ef => ef.Candidate)
                .Where(cs => cs.CriterionId == criterionId)
                .OrderByDescending(cs => cs.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<CriterionScoreDto>>(scores);
        }

        public async Task<CriterionScoreDto> CreateAsync(CreateCriterionScoreRequest request)
        {

            var criterion = await _context.EvaluationCriteria.FindAsync(request.CriterionId);
            if (criterion == null)
                throw new ArgumentException($"Criterion with ID {request.CriterionId} not found");

            if (request.Score < 1 || request.Score > 10)
                throw new ArgumentException("Score must be between 1 and 10");
            var score = _mapper.Map<CriterionScore>(request);

            _context.CriterionScores.Add(score);
            await _context.SaveChangesAsync();

            var createdScore = await _context.CriterionScores
                .Include(cs => cs.Criterion)
                .Include(cs => cs.EvaluationForm)
                .FirstOrDefaultAsync(cs => cs.Id == score.Id);

            return _mapper.Map<CriterionScoreDto>(createdScore);
        }

        public async Task<CriterionScoreDto> UpdateAsync(Guid id, UpdateCriterionScoreRequest request)
        {
            var score = await _context.CriterionScores.FindAsync(id);

            if (score == null)
                throw new KeyNotFoundException($"Score with ID {id} not found");

            _mapper.Map(request, score);
            await _context.SaveChangesAsync();
            return _mapper.Map<CriterionScoreDto>(score);


        }

        public async Task DeleteAsync(Guid id)
        {
            var score = await _context.CriterionScores
                .Include(cs => cs.EvaluationForm)
                .FirstOrDefaultAsync(cs => cs.Id == id);

            if (score == null)
                throw new KeyNotFoundException($"Criterion score with ID {id} not found");

            _context.CriterionScores.Remove(score);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CriterionScoreDto>> GetByCandidateAsync(Guid candidateId)
        {
            var candidateExists = await _context.Candidates.AnyAsync(c => c.Id == candidateId);
            if (!candidateExists)
                throw new ArgumentException($"Candidate with ID {candidateId} not found");

            var scores = await _context.CriterionScores
                .Include(cs => cs.Criterion)
                .Include(cs => cs.EvaluationForm)
                 .ThenInclude(ef => ef.Candidate)
                .Where(cs => cs.EvaluationForm.CandidateId == candidateId)
                .OrderByDescending(cs => cs.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<CriterionScoreDto>>(scores);
        }

    }
}