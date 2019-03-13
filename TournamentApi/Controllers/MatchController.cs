using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentApi.Models;

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        private readonly IMatchRepository _matchRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        [HttpGet]
        public IEnumerable<Match> GetMatches()
        {
            return _matchRepository.GetAll();
        }

        [HttpGet("{userId}")]
        public IEnumerable<Match> getMatchesFromPlayer(int userId)
        {
            return _matchRepository.GetAllFromPlayer(userId);
        }

        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            Match match = _matchRepository.GetById(id);

            if (match == null)
                return NotFound();

            return match;
        }

        [HttpPost]
        public ActionResult<Match> AddMatch(Match match)
        {
            _matchRepository.Add(match);
            _matchRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMatch), new { id = match.MatchId }, match);
        }

        [HttpPut("{id}")]
        public IActionResult EditMatch(int id, Match match)
        {
            if (id != match.MatchId)
                return NotFound();

            _matchRepository.Update(match);
            _matchRepository.SaveChanges();

            return NoContent();
        }

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