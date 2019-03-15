using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.Data.Mappers
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournaments");

            builder.HasMany(t => t.Participants)
                .WithOne(u => u.Tournament)
                .IsRequired(false)
                .HasForeignKey(u => u.TournamentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.Matches)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
