using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InterviewService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InterviewDto> GetByIdAsync(Guid id)
        {
            var interview = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .Include(i => i.EvaluationForm)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
                throw new KeyNotFoundException($"Interview with ID {id} not found");

            return _mapper.Map<InterviewDto>(interview);
        }

        public async Task<List<InterviewDto>> GetAllAsync()
        {
            var interviews = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .Include(i => i.EvaluationForm)
                .OrderByDescending(i => i.ScheduledAt)
                .ToListAsync();

            return _mapper.Map<List<InterviewDto>>(interviews);
        }

        public async Task<List<InterviewDto>> GetByCandidateAsync(Guid candidateId)
        {
            var candidateExists = await _context.Candidates.AnyAsync(c => c.Id == candidateId);
            if (!candidateExists)
                throw new ArgumentException($"Candidate with ID {candidateId} not found");

            var interviews = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .Include(i => i.EvaluationForm)
                .Where(i => i.CandidateId == candidateId)
                .OrderByDescending(i => i.ScheduledAt)
                .ToListAsync();

            return _mapper.Map<List<InterviewDto>>(interviews);
        }

        public async Task<List<InterviewDto>> GetByVacancyAsync(Guid vacancyId)
        {
            var vacancyExists = await _context.Vacancies.AnyAsync(v => v.Id == vacancyId);
            if (!vacancyExists)
                throw new ArgumentException($"Vacancy with ID {vacancyId} not found");

            var interviews = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .Include(i => i.EvaluationForm)
                .Where(i => i.VacancyId == vacancyId)
                .OrderByDescending(i => i.ScheduledAt)
                .ToListAsync();

            return _mapper.Map<List<InterviewDto>>(interviews);
        }

        public async Task<List<InterviewDto>> GetByInterviewerAsync(Guid interviewerId)
        {
            var interviewerExists = await _context.Users.AnyAsync(u => u.Id == interviewerId);
            if (!interviewerExists)
                throw new ArgumentException($"User with ID {interviewerId} not found");

            var interviews = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .Include(i => i.EvaluationForm)
                .Where(i => i.InterviewerUserId == interviewerId)
                .OrderByDescending(i => i.ScheduledAt)
                .ToListAsync();

            return _mapper.Map<List<InterviewDto>>(interviews);
        }

        public async Task<InterviewDto> CreateAsync(CreateInterviewRequest request)
        {
            var candidateExists = await _context.Candidates.AnyAsync(c => c.Id == request.CandidateId);
            if (!candidateExists)
                throw new ArgumentException($"Candidate with ID {request.CandidateId} not found");

            var vacancy = await _context.Vacancies
                 .FirstOrDefaultAsync(v => v.Id == request.VacancyId);
            if (vacancy == null)
                throw new ArgumentException($"Vacancy with ID {request.VacancyId} not found");

            if (vacancy.IsClosed)
                throw new InvalidOperationException($"Cannot create interview for closed vacancy (ID: {vacancy.Id})");

            var interviewerExists = await _context.Users.AnyAsync(u => u.Id == request.InterviewerUserId);
            if (!interviewerExists)
                throw new ArgumentException($"User with ID {request.InterviewerUserId} not found");

            var interview = _mapper.Map<Interview>(request);

            //кто назначил собес (пока используем того же что и проводит, позже поменять на текущую сессию)
            interview.CreatedByUserId = request.InterviewerUserId;

            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            var createdInterview = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .FirstOrDefaultAsync(i => i.Id == interview.Id);

            return _mapper.Map<InterviewDto>(createdInterview);
        }

        public async Task<InterviewDto> UpdateAsync(Guid id, UpdateInterviewRequest request)
        {
            var interview = await _context.Interviews
                .Include(i => i.Candidate)
                .Include(i => i.Vacancy)
                .Include(i => i.CreatedBy)
                .Include(i => i.Interviewer)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
                throw new KeyNotFoundException($"Interview with ID {id} not found");

            if (request.InterviewerUserId.HasValue)
            {
                var interviewerExists = await _context.Users.AnyAsync(u => u.Id == request.InterviewerUserId.Value);
                if (!interviewerExists)
                    throw new ArgumentException($"User with ID {request.InterviewerUserId} not found");
            }

            _mapper.Map(request, interview);
            await _context.SaveChangesAsync();

            return _mapper.Map<InterviewDto>(interview);
        }

        public async Task DeleteAsync(Guid id)
        {
            var interview = await _context.Interviews
                .Include(i => i.EvaluationForm)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
                throw new KeyNotFoundException($"Interview with ID {id} not found");

            // Проверяем, есть ли связанная форма оценки
            if (interview.EvaluationForm != null)
                throw new InvalidOperationException("Cannot delete interview with associated evaluation form");

            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(Guid id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
                throw new KeyNotFoundException($"Interview with ID {id} not found");

            interview.Completed = true;
            await _context.SaveChangesAsync();
        }
    }
}