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
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.Description)
                .HasMaxLength(2000);

            builder.Property(v => v.IsClosed)
                .HasDefaultValue(false);

            builder.Property(v => v.ClosedAt)
                .IsRequired(false);

            builder.HasIndex(v => v.Title);
            builder.HasIndex(v => v.IsClosed);
        }
    }
}
