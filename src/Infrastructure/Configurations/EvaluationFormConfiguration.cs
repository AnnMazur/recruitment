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
    public class EvaluationFormConfiguration : IEntityTypeConfiguration<EvaluationForm>
    {
        public void Configure(EntityTypeBuilder<EvaluationForm> builder)
        {
            builder.HasKey(ef => ef.Id);

            builder.Property(ef => ef.Name).IsRequired().HasMaxLength(200);
            builder.Property(ef => ef.Description).HasMaxLength(1000);
            builder.Property(ef => ef.Recommendation).IsRequired().HasDefaultValue(0);
            builder.Property(ef => ef.TotalScore).HasColumnType("decimal(4,2)").HasDefaultValue(0.0);
            builder.Property(ef => ef.OverallComment)
                .HasMaxLength(2000);

            builder.HasOne(ef => ef.Candidate)
                .WithMany(c => c.EvaluationForms)
                .HasForeignKey(ef => ef.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ef => ef.Interviewer)
                .WithMany() 
                .HasForeignKey(ef => ef.InterviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ef => ef.Interview)
                .WithOne(i => i.EvaluationForm)
                .HasForeignKey<EvaluationForm>(ef => ef.InterviewId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ef => ef.InterviewId).IsUnique();
            builder.HasIndex(ef => ef.CandidateId);
            builder.HasIndex(ef => ef.InterviewerId);
            builder.HasIndex(ef => ef.TotalScore);
        }
    }
}

