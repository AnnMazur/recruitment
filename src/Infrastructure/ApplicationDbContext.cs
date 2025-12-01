using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<EvaluationForm> EvaluationForms { get; set; }
        public DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }
        public DbSet<CriterionScore> CriterionScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            modelBuilder.ApplyConfiguration(new InterviewConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationFormConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationCriterionConfiguration());
            modelBuilder.ApplyConfiguration(new CriterionScoreConfiguration());

            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }
}


