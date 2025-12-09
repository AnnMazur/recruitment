using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class EvaluationFormService : IEvaluationFormService 
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICandidateService _candidateService;

        public EvaluationFormService(IApplicationDbContext context, IMapper mapper, ICandidateService candidateService)
        {
            _context = context;
            _mapper = mapper;
            _candidateService = candidateService;
        }


        public async Task<EvaluationFormDto> GetByIdAsync(Guid id) {
        
            var evaluationForm = await _context.EvaluationForms
                .Include(ef => ef.Candidate)
                .Include(ef => ef.Interview)  
                .Include(ef => ef.Interviewer)
                .FirstOrDefaultAsync();

            if (evaluationForm == null)
                throw new KeyNotFoundException($"Evaluation form with ID {id} not found");

            return _mapper.Map<EvaluationFormDto>(evaluationForm);
        }
        public async Task<List<EvaluationFormDto>> GetAllAsync() {
            var evaluationForms = await _context.EvaluationForms
                    .Include(ef => ef.Candidate)
                    .Include(ef => ef.Interview)
                    .Include(ef => ef.Interviewer)
                    .ToListAsync();

            return _mapper.Map<List<EvaluationFormDto>>(evaluationForms);
        }
        public async Task<List<EvaluationFormDto>> GetByCandidateAsync(Guid candidateId) {

            var candidateExists = await _context.Candidates.AnyAsync(c => c.Id == candidateId);
            if (!candidateExists)
                throw new ArgumentException($"Candidate with ID {candidateId} not found");
            
            var evaluationForms = await _context.EvaluationForms
                  .Include(ef => ef.Candidate)
                  .Include(ef => ef.Interview)
                  .Include(ef => ef.Interviewer)
                  .Where(ef => ef.CandidateId == candidateId)
                  .ToListAsync();

            return _mapper.Map<List<EvaluationFormDto>>(evaluationForms);

        }
        public async Task<List<EvaluationFormDto>> GetByInterviewerAsync(Guid interviewerId) {

            var interviewerExists = await _context.Candidates.AnyAsync(i => i.Id == interviewerId);
            if (!interviewerExists)
                throw new ArgumentException($"Candidate with ID {interviewerId} not found");

            var evaluationForms = await _context.EvaluationForms
                  .Include(ef => ef.Candidate)
                  .Include(ef => ef.Interview)
                  .Include(ef => ef.Interviewer)
                  .Where(ef => ef.Id == interviewerId)
                  .ToListAsync();

            return _mapper.Map<List<EvaluationFormDto>>(evaluationForms);
        }
        public async Task<List<EvaluationFormDto>> GetByInterviewAsync(Guid interviewId) {

            var interviewExists = await _context.Candidates.AnyAsync(i => i.Id == interviewId);
            if (!interviewExists)
                throw new ArgumentException($"Candidate with ID {interviewId} not found");

            var evaluationForms = await _context.EvaluationForms
                  .Include(ef => ef.Candidate)
                  .Include(ef => ef.Interview)
                  .Include(ef => ef.Interviewer)
                  .Where(ef => ef.Id == interviewId)
                  .ToListAsync();

            return _mapper.Map<List<EvaluationFormDto>>(evaluationForms);
        }

        public async Task<EvaluationFormDto> CreateAsync(CreateEvaluationFormRequest request) {

            var interview = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Interviewer)
                .FirstOrDefaultAsync(i => i.Id == request.InterviewId);

            if (interview == null)
                throw new ArgumentException($"Interview with ID {request.InterviewId} not found");


            var criterionIds = request.Scores?.Select(s => s.CriterionId).Distinct().ToList() ?? new List<Guid>();

            var criteria = await _context.EvaluationCriteria
                .Where(c => criterionIds.Contains(c.Id))
                .ToDictionaryAsync(c => c.Id);

            var evaluationForm = new EvaluationForm
            {
                Name = request.Name,
                Description = request.Description,
                Recommendation = request.Recommendation,
                OverallComment = request.OverallComment,
                CandidateId = interview.CandidateId,
                InterviewerId = interview.InterviewerUserId,
                InterviewId = request.InterviewId
            };

            if (request.Scores != null && request.Scores.Any())
            {
                foreach (var scoreRequest in request.Scores)
                {
                    if (!criteria.TryGetValue(scoreRequest.CriterionId, out var criterion))
                        throw new ArgumentException($"Criterion with ID {scoreRequest.CriterionId} not found");

                    if (scoreRequest.Score < 1 || scoreRequest.Score > 10)
                        throw new ArgumentException($"Score must be between 1 and 10 for criterion {criterion.Name}");

                    var score = new CriterionScore
                    {
                        CriterionId = scoreRequest.CriterionId,
                        Score = scoreRequest.Score,
                        Comment = scoreRequest.Comment
                    };

                    evaluationForm.Scores.Add(score);
                }
            }

            evaluationForm.TotalScore = CalculateTotalScore(evaluationForm, criteria);

            _context.EvaluationForms.Add(evaluationForm);
            await _context.SaveChangesAsync();

            await _candidateService.UpdateCandidateRatingAsync(interview.CandidateId);

            var createdForm = await _context.EvaluationForms
                .Include(ef => ef.Candidate)
                .Include(ef => ef.Interview)
                .Include(ef => ef.Interviewer)
                .Include(ef => ef.Scores)
                    .ThenInclude(s => s.Criterion)
                .FirstOrDefaultAsync(ef => ef.Id == evaluationForm.Id);

            return _mapper.Map<EvaluationFormDto>(createdForm);

        }

     public async Task<EvaluationFormDto> UpdateAsync(Guid id, UpdateEvaluationFormRequest request) {

            var evaluationForm = await _context.EvaluationForms
               .Include(ef => ef.Candidate)
               .Include(ef => ef.Interview)
               .Include(ef => ef.Interviewer)
               .FirstOrDefaultAsync(ef => ef.Id == id);

            if (evaluationForm == null)
                throw new KeyNotFoundException($"Interview with ID {id} not found");


            _mapper.Map(request, evaluationForm);
            await _context.SaveChangesAsync();

            return _mapper.Map<EvaluationFormDto>(evaluationForm);
        }
    

        public async Task DeleteAsync(Guid id)
        {
            var evaluationForm = await _context.EvaluationForms
              .Include(ef => ef.Candidate)
              .Include(ef => ef.Interview)
              .Include(ef => ef.Interviewer)
              .FirstOrDefaultAsync(ef => ef.Id == id);

            if (evaluationForm == null)
                throw new KeyNotFoundException($"EvaluationForm with ID {id} not found");

            _context.EvaluationForms.Remove(evaluationForm);
            await _context.SaveChangesAsync();

        }

        private double CalculateTotalScore(EvaluationForm evaluationForm, Dictionary<Guid, EvaluationCriterion> criteria = null)
        {

            var allScores = new List<double>();

            allScores.Add(evaluationForm.Recommendation);

            if (evaluationForm.Scores != null && evaluationForm.Scores.Any())
            {
                foreach (var score in evaluationForm.Scores)
                {
                    if (criteria.TryGetValue(score.CriterionId, out var criterion))
                    {

                       double criterionScore = score.Score * criterion.Weight;
                        allScores.Add(criterionScore);
                    }
                }
            }

            if (!allScores.Any()) return 0;

            return allScores.Average();
        }

    }
     
}
