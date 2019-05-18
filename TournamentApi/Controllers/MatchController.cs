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
    //[AllowAnonymous]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatchController : ControllerBase
    {

        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;

        public MatchController(IMatchRepository matchRepository, IUserRepository userRepository)
        {
            _matchRepository = matchRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gives all matches 
        /// </summary>
        /// <returns>All matches</returns>
        [HttpGet]
        public IEnumerable<MatchDTO> GetMatches()
        {
            return _matchRepository.GetAll();            
        }


        /// <summary>
        /// Gives all matches from a player
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>all matches of u player</returns>
        [HttpGet("GetMatchesVanSpeler/{userId}")]
        public IEnumerable<MatchDTO> GetMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetAll().Where(m => m.WinnerId == userId  || m.LoserId == userId);
        }//aanpassen zodat gefilterd wordt in repo, en niet in controller

        /// <summary>
        /// Gives all the matches that a user has won
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetWonMatchesFromPlayer/{userId}")]
        public IEnumerable<MatchDTO> GetWonMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetAll().Where(m => m.WinnerId == userId);
        }//aanpassen zodat gefilterd wordt in repo, en niet in controller

        /// <summary>
        /// Gives all the matches that a user has lost
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetLostMatchesFromPlayer/{userId}")]
        public IEnumerable<MatchDTO> GetLostMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetAll().Where(m => m.LoserId == userId);
        }//aanpassen zodat gefilterd wordt in repo, en niet in controller

        /// <summary>
        /// Gives the amount of lost matches of a user
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetAmountLostMatches/{userId}")]
        public ActionResult<int> getAmountLostMatches(int userId)
        {
            return _matchRepository.GetAll().Where(m => m.LoserId == userId).Count();
        }

        /// <summary>
        /// Gives the amount of lost matches of a user
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>returns a list of matches </returns>
        [HttpGet("GetAmountWonMatches/{userId}")]
        public ActionResult<int> getAmountwonMatches(int userId)
        {
            return _matchRepository.GetAll().Where(m => m.WinnerId == userId).Count();
        }

        /// <summary>
        /// Get a match
        /// </summary>
        /// <param name="id">id of the match</param>
        /// <returns>a match</returns>
        [HttpGet("{id}")]
        public ActionResult<MatchDTO> GetMatch(int id)
        {
            MatchDTO match = _matchRepository.GetById(id);

            if (match == null)
                return NotFound();

            return match;
        }

        ///// <summary>
        ///// creates a match
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult<Match> AddMatch(Match match)
        //{
        //    _matchRepository.Add(match);
        //    _matchRepository.SaveChanges();

        //    return CreatedAtAction(nameof(GetMatch), new { id = match.MatchId }, match);
        //}


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
        public ActionResult<MatchDTO> DeleteMatch(int id)
        {
            MatchDTO match = _matchRepository.GetById(id);

            if (match == null)
                return NotFound();

            _matchRepository.Delete(match.MatchId);
            _matchRepository.SaveChanges();

            return match;

        }
        /// <summary>
        /// insert a score of a finnished match
        /// </summary>
        /// <param name="match"></param>
        /// <returns>nothing</returns>
        [HttpPut("commitScore")]
        public IActionResult CommitScore (MatchDTO match)
        {
            Match selectedMatch = _matchRepository.GetByIdMatch(match.MatchId);
            User p1 = _userRepository.GetById(selectedMatch.Player1.UserId);
            User p2 = _userRepository.GetById(selectedMatch.Player2.UserId);

            selectedMatch.RegisterScore(
                    match.WinnerSet1,
                    match.LoserSet1,
                    match.WinnerSet2,
                    match.LoserSet2,
                    match.WinnerSet3,
                    match.LoserSet3);

            if (selectedMatch.Player1.UserId != selectedMatch.Winner.WinnerId)
            {
                

                if(p1.RankInTournament < p2.RankInTournament)
                {
                    int hulp = p2.RankInTournament;
                    p2.RankInTournament = p1.RankInTournament;
                    p1.RankInTournament = hulp;
                }
            }

            else
            {

                if (p2.RankInTournament < p1.RankInTournament)
                {
                    int hulp = p1.RankInTournament;
                    p1.RankInTournament = p2.RankInTournament;
                    p2.RankInTournament = hulp;
                }
            }

            p1.HasChallenge = false;
            p2.HasChallenge = false;

            _matchRepository.SaveChanges();
            return NoContent();
        }

    }
}