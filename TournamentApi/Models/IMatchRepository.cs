using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        IEnumerable<Match> GetAllFromPlayer(int userId);
        IEnumerable<Match> GetMatchesWonFromPlayer(int userId);
        IEnumerable<Match> GetMatchesLostFromPlayer(int userId);
        Match GetById(int id);
        void Add(Match match);
        void Delete(Match match);
        void Update(Match match);
        void SaveChanges();
    }
}
