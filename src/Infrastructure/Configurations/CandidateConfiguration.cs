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
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.SecondName)
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.ResumeUrl)
                .HasMaxLength(500);

            builder.Property(c => c.TotalRating)
                .HasDefaultValue(0.0)
                .HasColumnType("decimal(3,1)");

            // Связь с Vacancy (один-ко-многим)
            builder.HasOne(c => c.Vacancy)
                .WithMany(v => v.Candidates)
                .HasForeignKey(c => c.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Email);
            builder.HasIndex(c => new { c.LastName, c.FirstName });
            builder.HasIndex(c => c.VacancyId);
        }
    }
}



