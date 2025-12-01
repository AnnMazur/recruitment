using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{

    public class CandidateService : ICandidateService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CandidateService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CandidateDto> GetByIdAsync(Guid id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Vacancy)
                .Include(c => c.Interviews)
                .Include(c => c.EvaluationForms)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
                throw new KeyNotFoundException($"Candidate with ID {id} not found");

            return _mapper.Map<CandidateDto>(candidate);
        }
        public async Task<List<CandidateDto>> GetAllAsync()
        {
            var candidates = await _context.Candidates
               .Include(c => c.Vacancy)
               .Include(c => c.Interviews)
               .Include(c => c.EvaluationForms)
               .OrderByDescending(c => c.CreatedAt)
               .ToListAsync();

            return _mapper.Map<List<CandidateDto>>(candidates);

        }
        public async Task<List<CandidateDto>> GetByVacancyAsync(Guid vacancyId)
        {
            var vacancyExists = await _context.Vacancies.AnyAsync(v => v.Id == vacancyId);
            if (!vacancyExists)
                throw new ArgumentException($"Vacancy with ID {vacancyId} not found");

            var candidates = await _context.Candidates
              .Include(c => c.Vacancy)
              .Include(c => c.Interviews)
              .Where(c => c.VacancyId == vacancyId)
              .OrderByDescending(c => c.CreatedAt)
              .ToListAsync();

            return _mapper.Map<List<CandidateDto>>(candidates);
        }
        public async Task<CandidateDto> CreateAsync(CreateCandidateRequest request)
        {
            var emailExists = await _context.Candidates.AnyAsync(c => c.Email == request.Email);
            if (emailExists)
                throw new ArgumentException($"Candidate with email {request.Email} already exists");

            var vacancyExists = await _context.Vacancies.AnyAsync(v => v.Id == request.VacancyId);
            if (!vacancyExists)
                throw new ArgumentException($"Vacancy with ID {request.VacancyId} not found");

            var candidate = _mapper.Map<Candidate>(request);

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return _mapper.Map<CandidateDto>(candidate);
        }
        public async Task<CandidateDto> UpdateAsync(Guid id, UpdateCandidateRequest request)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate==null)
                throw new KeyNotFoundException($"Candidate with ID {id} not found");

            _mapper.Map(request, candidate);
            await _context.SaveChangesAsync();
            return _mapper.Map<CandidateDto>(candidate);

        }
        public async Task DeleteAsync(Guid id) 
        { 
            var candidate = await _context.Candidates
                .Include (c => c.Interviews)
                .FirstOrDefaultAsync(c => c.Id == id);

            _context.Candidates.Remove(candidate); 
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCandidateRatingAsync(Guid candidateId)
        {
            var candidate = await _context.Candidates
                .Include(c => c.EvaluationForms)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate!=null && candidate.EvaluationForms.Any())
            {
                candidate.TotalRating = candidate.EvaluationForms.Average(ef => ef.TotalScore);
                await _context.SaveChangesAsync();
            }

            
        }
    }
}