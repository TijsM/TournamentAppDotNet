using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using TournamentApi.DTO_s;

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class TournamentController : ControllerBase
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchRepository _matchRepository;

        public TournamentController(ITournamentRepository tournamentRepository, IUserRepository userRepository, IMatchRepository matchRepository)
        {
            _tournamentRepository = tournamentRepository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;


        }
        /// <summary>
        /// get all Tournaments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TournamentDTO> GetTournaments()
        {
            return _tournamentRepository.GetAll();
            
        }


        /// <summary>
        /// get a Tournament with given id
        /// </summary>
        /// <param name="id">id of the tournament</param>
        /// <returns>one tournament</returns>
        [HttpGet("{id}")]
        public ActionResult<TournamentDTO> GetTournament(int id)
        {
            TournamentDTO tournament = _tournamentRepository.GetByIdDTO(id);

            if (tournament == null)
                return NotFound();

            return tournament;
        }

        /// <summary>
        /// Adds a tournament
        /// </summary>
        /// <param name="tournament">Tournament</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Tournament> AddTournament(Tournament tournament)
        {
            _tournamentRepository.Add(tournament);
            _tournamentRepository.SaveChanges();

            return CreatedAtAction(nameof(GetTournament), new { id = tournament.TournamentId }, tournament);
        }



        /// <summary>
        /// Eddits a tournament
        /// </summary>
        /// <param name="id">Id of the tournament which you want to change</param>
        /// <param name="tournament"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditTournament(int id, Tournament tournament)
        {
            if (id != tournament.TournamentId)
                return BadRequest();

            _tournamentRepository.update(tournament);
            _tournamentRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// removes a tournament
        /// </summary>
        /// <param name="id">id of the to deleted tournament</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<TournamentDTO> DeleteTournament(int id)
        {
            TournamentDTO tournament = _tournamentRepository.GetByIdDTO(id);

            if (tournament == null)
                return NotFound();

            _tournamentRepository.Delete(tournament.TournamentId);
            _tournamentRepository.SaveChanges();
            return tournament;

        }

        /// <summary>
        /// Creates a match within a tournament
        /// </summary>
        /// <param name="tournamentId">the id of the tournament</param>
        /// <param name="player1Id">the id of the loged in player</param>
        /// <param name="player2Id">th id of the challenged player</param>
        /// <returns></returns>
        [HttpPost("AddMatchToTournament")]
        public IActionResult AddMatchToTournament(TournamentDTO dto)
        {
            var selectedTournament = _tournamentRepository.GetById(dto.TournamentId);

            User p1 = _userRepository.GetById(dto.Player1Id);
            p1.HasChallenge = true;  
            User p2 = _userRepository.GetById(dto.Player2Id);
            p2.HasChallenge = true;

            Match m = new Match(p1, p2);
            selectedTournament.AddMatch(m);

            _matchRepository.Add(m);
            _tournamentRepository.SaveChanges();

            addPendingMatchToUsers(p1, p2);

            return NoContent();
        }

        private Match createPendingMatch(Match m)
        {
            return new Match()
            {
                MatchId = m.MatchId,
                UserMatches = m.UserMatches
            };
        }

        private void addPendingMatchToUsers(User p1, User p2)
        {

            Match theNewMatch = _matchRepository.GetMatchWithMaxId();


            p1.PendingMatch = theNewMatch;
            p2.PendingMatch = theNewMatch;
            _tournamentRepository.SaveChanges();
        }
    }
}