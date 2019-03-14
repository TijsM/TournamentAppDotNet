using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TournamentApi.Models;

namespace TournamentApi.Data
{
    public class TournamentAppDataInitializer
    {
        #region Fields
        private readonly TournamentContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IList<Match> _matches;
        #endregion


        #region Constructor
        public TournamentAppDataInitializer(TournamentContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        #endregion


        #region Methods
        public async Task initializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                Tournament tournamentMen = new Tournament { Name = "TournamentMen", EndDate = new DateTime(2019, 06, 01), StartDate = new DateTime(2019, 08, 31), Gender = Gender.Man };
                Tournament tournamentWomen = new Tournament { Name = "TournamentWomen", EndDate = new DateTime(2019, 06, 01), StartDate = new DateTime(2019, 08, 31), Gender = Gender.woman };
                _dbContext.Tournaments.Add(tournamentMen);
                _dbContext.Tournaments.Add(tournamentWomen);

                User RafaelNadal = new User { FirstName = "Rafael", FamilyName = "Nadal", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1987, 06, 14), PhoneNumber = "+33499875447", Email = "rafael.nadal@gmail.com", Gender = Gender.Man };
                User RogerFederer = new User { FirstName = "Roger", FamilyName = "Federer", TennisVlaanderenRanking = 15, DateOfBirth = new DateTime(1983, 02, 28), PhoneNumber = "+02485321456", Email = "roger.federer@gmail.com", Gender = Gender.Man };
                User NovakDjokovich = new User { FirstName = "Novak", FamilyName = "Djokovich", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1990, 02, 28), PhoneNumber = "+38458623554", Email = "novak.djokovich@gmail.com", Gender = Gender.Man };

                User KimKlijsters = new User { FirstName = "Kim", FamilyName = "Klijsters", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1985, 01, 02), PhoneNumber = "+32544785447", Email = "kim.klijsters@gmail.com", Gender = Gender.woman };
                User MariaCharapova = new User { FirstName = "Maria", FamilyName = "Charapova", TennisVlaanderenRanking = 15, DateOfBirth = new DateTime(1993, 11, 09), PhoneNumber = "+32458745221", Email = "maria.charapova@gmail.com", Gender = Gender.woman };
                User JustineHenin = new User { FirstName = "Justine", FamilyName = "Henin", TennisVlaanderenRanking = 20, DateOfBirth = new DateTime(1983, 03, 09), PhoneNumber = "+32488754221", Email = "justine.henin@gmail.com", Gender = Gender.woman };

                tournamentMen.Participants.Add(RafaelNadal);
                tournamentMen.Participants.Add(RogerFederer);
                tournamentMen.Participants.Add(NovakDjokovich);


                tournamentWomen.Participants.Add(KimKlijsters);
                tournamentWomen.Participants.Add(MariaCharapova);
                tournamentWomen.Participants.Add(JustineHenin);

                Match match1 = new Match(RafaelNadal, RogerFederer);
                Match match2 = new Match(RafaelNadal, NovakDjokovich);
                Match match3 = new Match(RogerFederer, RafaelNadal);
                Match match4 = new Match(RogerFederer, NovakDjokovich);
                Match match5 = new Match(NovakDjokovich, RafaelNadal);
                Match match6 = new Match(NovakDjokovich, RogerFederer);

                Match match7 = new Match(KimKlijsters, MariaCharapova);
                Match match8 = new Match(KimKlijsters, JustineHenin);
                Match match9 = new Match(MariaCharapova, KimKlijsters);
                Match match10 = new Match(MariaCharapova, JustineHenin);
                Match match11 = new Match(JustineHenin, KimKlijsters);
                Match match12 = new Match(JustineHenin, MariaCharapova);

                _dbContext.Matches.AddRange(match1
                    , match2
                    , match3
                    , match4
                    , match5
                    , match6
                    , match7
                    , match8
                    , match9
                    , match10
                    , match11
                    , match12
                    );
                    


                foreach(User u in tournamentMen.Participants)
                {
                    await CreateUser(u.Email, "P@ssword1111");

                }

                foreach(User u in tournamentWomen.Participants)
                {
                    await CreateUser(u.Email, "P@ssword1111");
                }

                _dbContext.SaveChanges();


            }  
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
        #endregion
    }
}
