using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext 
    {
        DbSet<User> Users { get; set; }
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Vacancy> Vacancies { get; set; }
        DbSet<Interview> Interviews { get; set; }
        DbSet<EvaluationForm> EvaluationForms { get; set; }
        DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }
        DbSet<CriterionScore> CriterionScores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}