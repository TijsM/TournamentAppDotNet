using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.Data.Mappers
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");
            builder.HasKey(m => m.MatchId);

            builder.HasOne(m => m.Player1)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(m => m.Player2)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(m => m.Winner)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(m => m.Loser)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(m => m.Tournament)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
