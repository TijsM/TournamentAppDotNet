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
                #region Tournament
                Tournament tournamentMen = new Tournament { Name = "TournamentMen", EndDate = new DateTime(2019, 06, 01), StartDate = new DateTime(2019, 08, 31), Gender = Gender.Man };
                Tournament tournamentWomen = new Tournament { Name = "TournamentWomen", EndDate = new DateTime(2019, 06, 01), StartDate = new DateTime(2019, 08, 31), Gender = Gender.woman };
                _dbContext.Tournaments.Add(tournamentMen);
                _dbContext.Tournaments.Add(tournamentWomen);
                #endregion

                #region Users
                User RafaelNadal = new User { FirstName = "Rafael", FamilyName = "Nadal", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1987, 06, 14), PhoneNumber = "+33499875447", Email = "rafael.nadal@gmail.com", Gender = Gender.Man };
                User RogerFederer = new User { FirstName = "Roger", FamilyName = "Federer", TennisVlaanderenRanking = 15, DateOfBirth = new DateTime(1983, 02, 28), PhoneNumber = "+02485321456", Email = "roger.federer@gmail.com", Gender = Gender.Man };
                User NovakDjokovich = new User { FirstName = "Novak", FamilyName = "Djokovich", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1990, 02, 28), PhoneNumber = "+38458623554", Email = "novak.djokovich@gmail.com", Gender = Gender.Man };
                User DavidGofin = new User { FirstName = "David", FamilyName = "Gofin", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1996, 07, 12), PhoneNumber = "+32499875214", Email = "david.gofin@tv.be", Gender = Gender.Man };
                User AndyMurray = new User { FirstName = "Andy", FamilyName = "Murray", TennisVlaanderenRanking = 30, DateOfBirth = new DateTime(1988, 01, 12), PhoneNumber = "+32577896541", Email = "Andy@wimbledon.com", Gender = Gender.Man };

                User KimKlijsters = new User { FirstName = "Kim", FamilyName = "Klijsters", TennisVlaanderenRanking = 10, DateOfBirth = new DateTime(1985, 01, 02), PhoneNumber = "+32544785447", Email = "kim.klijsters@gmail.com", Gender = Gender.woman };
                User MariaCharapova = new User { FirstName = "Maria", FamilyName = "Charapova", TennisVlaanderenRanking = 15, DateOfBirth = new DateTime(1993, 11, 09), PhoneNumber = "+32458745221", Email = "maria.charapova@gmail.com", Gender = Gender.woman };
                User JustineHenin = new User { FirstName = "Justine", FamilyName = "Henin", TennisVlaanderenRanking = 20, DateOfBirth = new DateTime(1983, 03, 09), PhoneNumber = "+32488754221", Email = "justine.henin@gmail.com", Gender = Gender.woman };
                User SerinaWilliams = new User { FirstName = "Serina", FamilyName = "Williams", TennisVlaanderenRanking = 30, DateOfBirth = new DateTime(1980, 03, 09), PhoneNumber = "+32466321445", Email = "serena.williams@usa.com", Gender = Gender.woman };
                User AllisonVanuytvank = new User { FirstName = "Allison", FamilyName = "Vanuytvank", TennisVlaanderenRanking = 30, DateOfBirth = new DateTime(1996, 03, 01), PhoneNumber = "+32499875441", Email = "alisson@clijsteracademy.com", Gender = Gender.woman };


                tournamentMen.AddUser(RafaelNadal);
                tournamentMen.AddUser(RogerFederer);
                tournamentMen.AddUser(NovakDjokovich);
                tournamentMen.AddUser(DavidGofin);
                tournamentMen.AddUser(AndyMurray);
               
               
                tournamentWomen.AddUser(KimKlijsters);
                tournamentWomen.AddUser(MariaCharapova);
                tournamentWomen.AddUser(JustineHenin);
                tournamentWomen.AddUser(SerinaWilliams);
                tournamentWomen.AddUser(AllisonVanuytvank);
              
                #endregion

                #region Matches
                Match match1 = new Match(RafaelNadal, RogerFederer);
                Match match2 = new Match(RafaelNadal, NovakDjokovich);
                Match match3 = new Match(RogerFederer, RafaelNadal);
                Match match4 = new Match(RogerFederer, NovakDjokovich);
                Match match5 = new Match(NovakDjokovich, RafaelNadal);
                Match match6 = new Match(NovakDjokovich, RogerFederer);
                Match match7 = new Match(DavidGofin, RogerFederer);
                Match match8 = new Match(DavidGofin, NovakDjokovich);
                Match match9 = new Match(AndyMurray, RogerFederer);
                Match match10 = new Match(AndyMurray, NovakDjokovich);
                Match match11 = new Match(DavidGofin, AndyMurray);
                
                Match match01 = new Match(KimKlijsters, MariaCharapova);
                Match match02 = new Match(KimKlijsters, JustineHenin);
                Match match03 = new Match(MariaCharapova, KimKlijsters);
                Match match04 = new Match(MariaCharapova, JustineHenin);
                Match match05 = new Match(JustineHenin, KimKlijsters);
                Match match06 = new Match(SerinaWilliams, MariaCharapova);
                Match match07 = new Match(SerinaWilliams, KimKlijsters);
                Match match08 = new Match(SerinaWilliams, JustineHenin);
                Match match09 = new Match(AllisonVanuytvank, MariaCharapova);
                Match match010 = new Match(AllisonVanuytvank, JustineHenin);
                Match match011 = new Match(AllisonVanuytvank, SerinaWilliams);


                tournamentMen.AddMatch(match1);
                tournamentMen.AddMatch(match2);
                tournamentMen.AddMatch(match3);
                tournamentMen.AddMatch(match4);
                tournamentMen.AddMatch(match5);
                tournamentMen.AddMatch(match6);
                tournamentMen.AddMatch(match7);
                tournamentMen.AddMatch(match8);
                tournamentMen.AddMatch(match9);
                tournamentMen.AddMatch(match10);
                tournamentMen.AddMatch(match11);

                tournamentWomen.AddMatch(match01);
                tournamentWomen.AddMatch(match02);
                tournamentWomen.AddMatch(match03);
                tournamentWomen.AddMatch(match04);
                tournamentWomen.AddMatch(match05);
                tournamentWomen.AddMatch(match06);
                tournamentWomen.AddMatch(match07);
                tournamentWomen.AddMatch(match08);
                tournamentWomen.AddMatch(match09);
                tournamentWomen.AddMatch(match010);
                tournamentWomen.AddMatch(match011);

                #region MatchResults
                match1.RegisterScore(6, 4, 4, 6, 0, 6);
                match2.RegisterScore(6, 4, 4, 6, 6, 0);
                match3.RegisterScore(2,6,6,2,7,6);
                match4.RegisterScore(0,6,4,6);
                match5.RegisterScore(2,6,6,4,7,6);
                match6.RegisterScore(6,4,6,4);
                match7.RegisterScore(3,6,3,6);
                match8.RegisterScore(3,6,3,6);
                match9.RegisterScore(6,0,6,0);
                match10.RegisterScore(6,2,5,7,6,0);
                match11.RegisterScore(0,6,2,6);

                match01.RegisterScore(0, 6, 0, 6);
                match02.RegisterScore(6, 2, 6, 4);
                match03.RegisterScore(7,6,6,7,7,6);
                match04.RegisterScore(6,1,6,4);
                match05.RegisterScore(0,6,3,6);
                match06.RegisterScore(2,6,5,7);
                match07.RegisterScore(6,4,3,6,6,4);
                match08.RegisterScore(7,5,6,2);
                match09.RegisterScore(6, 2, 6, 4);
                match010.RegisterScore(6, 4, 6, 1);
                match011.RegisterScore(0,6,6,0,6,0);

                //Match pendingMatch = new Match(NovakDjokovich, RafaelNadal);
                //tournamentMen.AddMatch(pendingMatch);
                
                #endregion


                #endregion

                #region Accounts
                foreach (User u in tournamentMen.Users)
                {
                    await CreateUser(u.Email, "P@ssword1111");

                }

                foreach (User u in tournamentWomen.Users)
                {
                    await CreateUser(u.Email, "P@ssword1111");
                } 
                #endregion




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
