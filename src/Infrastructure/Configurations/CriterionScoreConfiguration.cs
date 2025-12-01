using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CriterionScoreConfiguration : IEntityTypeConfiguration<CriterionScore> 
    {
        public void Configure(EntityTypeBuilder<CriterionScore> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Value)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(cs => cs.NumericValue)
                .IsRequired()
                .HasColumnType("decimal(3,1)");

            builder.Property(cs => cs.Comment)
                .HasMaxLength(1000);

            builder.HasOne(cs => cs.EvaluationForm)
                .WithMany(ef => ef.Scores)
                .HasForeignKey(cs => cs.EvaluationFormId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Criterion)
                .WithMany(ec => ec.Scores)
                .HasForeignKey(cs => cs.CriterionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(cs => cs.EvaluationFormId);
            builder.HasIndex(cs => cs.CriterionId);
            builder.HasIndex(cs => cs.NumericValue);
        }
    }
}

