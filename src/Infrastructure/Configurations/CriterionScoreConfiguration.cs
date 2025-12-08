
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class CriterionScoreConfiguration : IEntityTypeConfiguration<CriterionScore>
{
    public void Configure(EntityTypeBuilder<CriterionScore> builder)
    {
        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.Score)
            .IsRequired()
            .HasDefaultValue(0);

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

        //ограничение 1-10
        builder.HasCheckConstraint("CK_CriterionScore_Score_Range", "\"Score\" >= 1 AND \"Score\" <= 10");

        builder.HasIndex(cs => cs.EvaluationFormId);
        builder.HasIndex(cs => cs.CriterionId);
    }
}