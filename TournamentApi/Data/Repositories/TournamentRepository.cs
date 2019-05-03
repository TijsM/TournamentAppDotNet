using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.DTO_s;
using TournamentApi.Models;

namespace TournamentApi.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext _context;
        private readonly DbSet<Tournament> _tournaments;
        private readonly IUserRepository _userRepository;



        public TournamentRepository(TournamentContext context, IUserRepository userRepository )
        {
            _context = context;
            _tournaments = context.Tournaments;
            _userRepository = userRepository;
        }
        public void Add(Tournament tournament)
        {
            _tournaments.Add(tournament);
        }

        public void Delete(int id)
        {
             var t = _tournaments
                .SingleOrDefault(n => n.TournamentId == id);

            _tournaments.Remove(t);
        }

        public IEnumerable<TournamentDTO> GetAll()
        {
            var tournaments =  _tournaments
                .Include(t => t.Users)
                .ToList();

            return MakeTournamentDTOList(tournaments);
        }

        public TournamentDTO GetByIdDTO(int id)
        {
            var t =  _tournaments
                .Include(tour => tour.Users)
                .SingleOrDefault(tour => tour.TournamentId == id);

            return new TournamentDTO { TournamentId = t.TournamentId, EndDate = t.EndDate, Gender = t.Gender, Matches = t.Matches, Name = t.Name, StartDate = t.StartDate, Users = t.Users.Select(tour => new UserDetailDTO(tour)) };
        }

        public Tournament GetById(int id)
        {
            return _tournaments
                .Include(t => t.Users)
                .SingleOrDefault(t => t.TournamentId == id);

            
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void update(Tournament tournament)
        {
            _tournaments.Update(tournament);
        }

        private IList<TournamentDTO> MakeTournamentDTOList(List<Tournament> tournamentList)
        {
            IList<TournamentDTO> dtoList = new List<TournamentDTO>();
            foreach(var t in tournamentList)
            {
                dtoList.Add(new TournamentDTO { TournamentId = t.TournamentId, EndDate = t.EndDate, Gender = t.Gender, Matches = t.Matches, Name = t.Name, StartDate = t.StartDate, Users = t.Users.Select(tour => new UserDetailDTO(tour)) });
            }

            return dtoList;
        }

        public IEnumerable<UserDetailDTO> giveRanking(int tournamentId)
        {

            var users = _tournaments
                
                .Include(t => t.Users)
                 .ThenInclude(u => u.UserMatches)
                 .Include(t => t.Users)
                .ThenInclude(u => u.PendingMatch)
                .SingleOrDefault(t => t.TournamentId == tournamentId)
                .Users
                .OrderBy(u => u.RankInTournament)
                .ToList();



            return makeUserDTOList(users);


        }


        private IList<UserDetailDTO> makeUserDTOList(List<User> userLijst)
        {
            IList<UserDetailDTO> dtolijst = new List<UserDetailDTO>();
            foreach (var u in userLijst)
            {
                dtolijst.Add(makeUserDTO(u));
            }
            return dtolijst;
        }

        private UserDetailDTO makeUserDTO(User u)
        {
            return new UserDetailDTO(u);
        }


    }
}
