using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.DTO_s;
using TournamentApi.Models;

namespace TournamentApi.Data.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly TournamentContext _context;
        private readonly DbSet<Match> _matches;
        private readonly IUserRepository _userRepository;

        public MatchRepository(TournamentContext context, IUserRepository userRepository)
        {
            _context = context;
            _matches = context.Matches;
            this._userRepository = userRepository;
        }

        public void Add(Match match)
        {
            _matches.Add(match);
        }
        

        public Match GetMatchWithMaxId()
        {
            var matches = _matches
                .Include(m => m.UserMatches)
                .ThenInclude(um => um.User)
                .OrderBy(m => m.MatchId)
                .ToList();

            int ma  = matches.Max<Match>(m => m.MatchId);


            return GetByIdMatch(ma);

        }


        public void Delete(int id)
        {
            var match = _matches.SingleOrDefault(m => m.MatchId == id);


            _matches.Remove(match);
        }

        public IEnumerable<MatchDTO> GetAll()
        {
            var matches = _matches
               .Include(m => m.UserMatches)
               .ThenInclude(um => um.User)
               .OrderBy(m => m.MatchId)
               .ToList();

            return MakeMatchDTOList(matches);
        }



        public IEnumerable<MatchDTO> GetAllFromPlayer(int userId)
        {
            var matches = _matches
               .Include(m => m.UserMatches)
               .ThenInclude(um => um.User)
               .Where(m => m.Player1.UserId == userId || m.Player2.UserId == userId) //dit lijntje geeft error
               .OrderBy(m => m.MatchId)
               .ToList();

            return MakeMatchDTOList(matches);


        }

        public IEnumerable<MatchDTO> GetMatchesWonFromPlayer(int userId)
        {
            var matches = _matches
               .Where(m => m.Winner.UserId == userId)
               .Include(m => m.UserMatches)
               .ThenInclude(um => um.User)
               .OrderBy(m => m.MatchId)
                .ToList();

            return MakeMatchDTOList(matches);

        }
        

        public IEnumerable<MatchDTO> GetMatchesLostFromPlayer(int userId)
        {
            var matches = _matches
              .Where(m => m.Loser.UserId == userId)
              .Include(m => m.UserMatches)
              .ThenInclude(um => um.User)
              .OrderBy(m => m.MatchId)
              .ToList();

            return MakeMatchDTOList(matches);

        }

        public MatchDTO GetById(int id)
        {
            var match = _matches
               .Include(m => m.UserMatches)
               .ThenInclude(um => um.User)
               .SingleOrDefault(m => m.MatchId == id);

            return MakeMatchDTO(match);

        }



        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Match match)
        {
            _matches.Update(match);
        }



        private IList<MatchDTO> MakeMatchDTOList(List<Match> matchList)
        {
            IList<MatchDTO> dtolijst = new List<MatchDTO>();
            foreach (var match in matchList)
            {
                dtolijst.Add(MakeMatchDTO(match));
            }
            return dtolijst;
        }

        private MatchDTO MakeMatchDTO(Match match)
        {

            User winner = _userRepository.GetById(match.Winner.UserId);
            User loser = _userRepository.GetById(match.Loser.UserId);

            return new MatchDTO()
            {
                MatchId = match.MatchId,
                WinnerFullName = match.Winner.WinnerFullName,
                LoserFullName = match.Loser.LoserFullName,
                WinnerId = match.Winner.WinnerId,
                LoserId = match.Loser.LoserId,

                WinnerSet1 = match.Winner.GamesWonSet1,
                WinnerSet2 = match.Winner.GamesWonSet2,
                WinnerSet3 = match.Winner.GamesWonSet3,

                LoserSet1 = match.Loser.GamesWonSet1,
                LoserSet2 = match.Loser.GamesWonSet2,
                LoserSet3 = match.Loser.GamesWonSet3,

                WinnerEmail = winner.Email,
                WinnerPhoneNumber = winner.PhoneNumber,
                loserEmail = loser.Email,
                LoserPhoneNumber = loser.PhoneNumber
            };
        }

        public Match GetByIdMatch(int id)
        {
            return _matches
               .Include(m => m.UserMatches)
               .ThenInclude(um => um.User)
               .SingleOrDefault(m => m.MatchId == id);
        }

    }
}
