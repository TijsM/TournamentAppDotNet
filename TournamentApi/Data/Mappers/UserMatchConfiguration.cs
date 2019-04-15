using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.Data.Mappers
{
    public class UserMatchConfiguration : IEntityTypeConfiguration<UserMatch>
    {

        public void Configure(EntityTypeBuilder<UserMatch> builder)
        {
            builder.ToTable("UserMatch");
            builder.HasKey(um => new { um.MatchId, um.UserId });

            builder
                .HasOne(um => um.Match)
                .WithMany(m => m.UserMatches)
                .HasForeignKey(um => um.MatchId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(um => um.User)
                .WithMany(u => u.UserMatches)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
