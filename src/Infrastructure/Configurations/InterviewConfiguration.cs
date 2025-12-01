using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ScheduledAt)
                .IsRequired();

            builder.Property(i => i.Completed)
                .HasDefaultValue(false);

            builder.HasOne(i => i.Candidate)
                .WithMany(c => c.Interviews)
                .HasForeignKey(i => i.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Vacancy)
                .WithMany(v => v.Interviews)
                .HasForeignKey(i => i.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.CreatedBy)
                .WithMany(u => u.CreatedInterviews)
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Interviewer)
                .WithMany(u => u.ConductedInterviews)
                .HasForeignKey(i => i.InterviewerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с EvaluationForm (1-1)
            builder.HasOne(i => i.EvaluationForm)
            .WithOne(ef => ef.Interview)
            .HasForeignKey<EvaluationForm>(ef => ef.InterviewId) // FK в EvaluationForm
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(i => i.CandidateId);
            builder.HasIndex(i => i.VacancyId);
            builder.HasIndex(i => i.CreatedByUserId);
            builder.HasIndex(i => i.InterviewerUserId);
            builder.HasIndex(i => i.ScheduledAt);
            builder.HasIndex(i => i.Completed);
        }
    }

}

