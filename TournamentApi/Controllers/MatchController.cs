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

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatchController : ControllerBase
    {

        private readonly IMatchRepository _matchRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        /// <summary>
        /// Gives all matches 
        /// </summary>
        /// <returns>All matches</returns>
        [HttpGet]
        public IEnumerable<Match> GetMatches()
        {
            return _matchRepository.GetAll();
        }


        /// <summary>
        /// Gives all matches from a player
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>all matches of u player</returns>
        [HttpGet("GetMatchesVanSpeler/{userId}")]
        public IEnumerable<Match> GetMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetAllFromPlayer(userId);
        }

        /// <summary>
        /// Gives all the matches that a user has won
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetWonMatchesFromPlayer/{userId}")]
        public IEnumerable<Match> GetWonMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetMatchesWonFromPlayer(userId);
        }

        /// <summary>
        /// Gives all the matches that a user has lost
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetLostMatchesFromPlayer/{userId}")]
        public IEnumerable<Match> GetLostMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetMatchesLostFromPlayer(userId);
        }



        /// <summary>
        /// Get a match
        /// </summary>
        /// <param name="id">id of the match</param>
        /// <returns>a match</returns>
        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            Match match = _matchRepository.GetById(id);

            if (match == null)
                return NotFound();

            return match;
        }

        /// <summary>
        /// creates a match
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Match> AddMatch(Match match)
        {
            _matchRepository.Add(match);
            _matchRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMatch), new { id = match.MatchId }, match);
        }


        /// <summary>
        /// updates a match
        /// </summary>
        /// <param name="id">id of the match</param>
        /// <param name="match"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditMatch(int id, Match match)
        {
            if (id != match.MatchId)
                return NotFound();

            _matchRepository.Update(match);
            _matchRepository.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Removes a match
        /// </summary>
        /// <param name="id">id of the match</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<Match> DeleteMatch(int id)
        {
            Match match = _matchRepository.GetById(id);

            if (match == null)
                return NotFound();

            _matchRepository.Delete(match);
            _matchRepository.SaveChanges();

            return match;

        }

    }
}