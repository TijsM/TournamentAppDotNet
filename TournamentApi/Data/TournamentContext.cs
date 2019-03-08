using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Data.Mappers;
using TournamentApi.Models;

namespace TournamentApi.Data
{
    public class TournamentContext: IdentityDbContext
    {
        #region Fields
        public DbSet<User> Users_domain { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        #endregion


        #region Constructor
        public TournamentContext(DbContextOptions<TournamentContext> options) : base(options)
        {

        }
        #endregion


        #region Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TournamentConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        } 
        #endregion
    }
}
