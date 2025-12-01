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

        public EvaluationFormService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

          /*  var candidateExists = await _context.Candidates.AnyAsync(c => c.Id == request.CandidateId);
            if (!candidateExists)
                throw new ArgumentException($"Candidate with ID {request.CandidateId} not found");

            var vacancyExists = await _context.Vacancies.AnyAsync(v => v.Id == request.VacancyId);
            if (!vacancyExists)
                throw new ArgumentException($"Vacancy with ID {request.VacancyId} not found");

            var interviewerExists = await _context.Users.AnyAsync(u => u.Id == request.InterviewerUserId);
            if (!interviewerExists)
                throw new ArgumentException($"User with ID {request.InterviewerUserId} not found");

            */
            var evaluationForm = _mapper.Map<EvaluationForm>(request);

            _context.EvaluationForms.Add(evaluationForm);
            await _context.SaveChangesAsync();

            var evaluationForms = await _context.EvaluationForms
               .Include(ef => ef.Candidate)
               .Include(ef => ef.Interview)
               .Include(ef => ef.Interviewer)
               .FirstOrDefaultAsync(ef => ef.Id == evaluationForm.Id);

            return _mapper.Map<EvaluationFormDto>(evaluationForms);



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

            //добавить проверку на наличие связаных оценок criterion score 

            if (evaluationForm == null)
                throw new KeyNotFoundException($"EvaluationForm with ID {id} not found");

            _context.EvaluationForms.Remove(evaluationForm);
            await _context.SaveChangesAsync();

        }
    } 

}
