using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking; 

namespace Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VacancyService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacancyDto> GetByIdAsync(Guid id)
        {
            var vacancy = await _context.Vacancies
                .Include(v => v.Candidates)
                .Include(v => v.Interviews)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacancy == null)
                throw new KeyNotFoundException($"Vacancy with ID {id} not found");

            return _mapper.Map<VacancyDto>(vacancy);
        }

        public async Task<List<VacancyDto>> GetAllAsync()
        {
            var vacancies = await _context.Vacancies
                .Include(v => v.Candidates)
                .Include(v => v.Interviews)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<VacancyDto>>(vacancies);
        }

        public async Task<List<VacancyDto>> GetActiveAsync()
        {
            var vacancies = await _context.Vacancies
                .Include(v => v.Candidates)
                .Include(v => v.Interviews)
                .Where(v => !v.IsClosed)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<VacancyDto>>(vacancies);
        }

        public async Task<VacancyDto> CreateAsync(CreateVacancyRequest request)
        {
            var vacancy = _mapper.Map<Vacancy>(request);

            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync();

            return _mapper.Map<VacancyDto>(vacancy);
        }

        public async Task<VacancyDto> UpdateAsync(Guid id, UpdateVacancyRequest request)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
                throw new KeyNotFoundException($"Vacancy with ID {id} not found");

            _mapper.Map(request, vacancy);

            // Обновляем время закрытия если меняем статус
            if (request.IsClosed == true && !vacancy.IsClosed)
            {
                vacancy.ClosedAt = DateTime.UtcNow;
            }
            else if (request.IsClosed == false && vacancy.IsClosed)
            {
                vacancy.ClosedAt = null;
            }

            await _context.SaveChangesAsync();

            // Загружаем связанные данные для возврата
            var updatedVacancy = await _context.Vacancies
                .Include(v => v.Candidates)
                .Include(v => v.Interviews)
                .FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<VacancyDto>(updatedVacancy);
        }

        public async Task DeleteAsync(Guid id)
        {
            var vacancy = await _context.Vacancies
                .Include(v => v.Candidates)
                .Include(v => v.Interviews)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacancy == null)
                throw new KeyNotFoundException($"Vacancy with ID {id} not found");

            if (vacancy.Candidates.Any())
                throw new InvalidOperationException("Cannot delete vacancy with associated candidates");

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task CloseAsync(Guid id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
                throw new KeyNotFoundException($"Vacancy with ID {id} not found");

            vacancy.IsClosed = true;
            vacancy.ClosedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}