using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.DTO_s;

namespace TournamentApi.Models
{
    public interface IMatchRepository
    {
        IEnumerable<MatchDTO> GetAll();
        IEnumerable<MatchDTO> GetAllFromPlayer(int userId);
        IEnumerable<MatchDTO> GetMatchesWonFromPlayer(int userId);
        IEnumerable<MatchDTO> GetMatchesLostFromPlayer(int userId);
        MatchDTO GetById(int id);
        void Add(Match match);
        void Delete(int id);
        void Update(Match match);
        void SaveChanges();
        Match GetMatchWithMaxId();
        Match GetByIdMatch(int id);


    }
}
