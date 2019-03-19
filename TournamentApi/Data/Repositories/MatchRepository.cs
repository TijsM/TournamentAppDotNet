using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.Data.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly TournamentContext _context;
        private readonly DbSet<Match> _matches;

        public MatchRepository(TournamentContext context)
        {
            _context = context;
            _matches = context.Matches;
        }

        public void Add(Match match)
        {
            _matches.Add(match);
        }

        public void Delete(Match match)
        {
            _matches.Remove(match);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matches
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .ToList();
        }

        public IEnumerable<Match> GetAllFromPlayer(int userId)
        {
            return _matches
                .Where(m => m.Player1.UserId == userId || m.Player2.UserId == userId)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .ToList();
        }

        public Match GetById(int id)
        {
            return _matches
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.UserMatches)
                .SingleOrDefault(m => m.MatchId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Match match)
        {
            _matches.Update(match);
        }
    }
}
