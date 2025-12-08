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
    public class EvaluationCriterionConfiguration : IEntityTypeConfiguration<EvaluationCriterion>
    {
        public void Configure(EntityTypeBuilder<EvaluationCriterion> builder)
        {
            builder.HasKey(ec => ec.Id);

            builder.Property(ec => ec.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ec => ec.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(ec => ec.Weight)
                .HasDefaultValue(1.0)
                .HasColumnType("decimal(3,2)");

            builder.HasIndex(ec => ec.Name).IsUnique(); 
        }
    }

}
