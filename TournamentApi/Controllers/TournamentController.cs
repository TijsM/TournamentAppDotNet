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

    public class TournamentController : ControllerBase
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }
        /// <summary>
        /// get all Tournaments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Tournament> GetTournaments()
        {
            return _tournamentRepository.GetAll();
        }


        /// <summary>
        /// get a Tournament with given id
        /// </summary>
        /// <param name="id">id of the tournament</param>
        /// <returns>one tournament</returns>
        [HttpGet("{id}")]
        public ActionResult<Tournament> GetTournament(int id)
        {
            Tournament tournament = _tournamentRepository.GetById(id);

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
        public ActionResult<Tournament> DeleteTournament(int id)
        {
            Tournament tournament = _tournamentRepository.GetById(id);

            if (tournament == null)
                return NotFound();

            _tournamentRepository.Delete(tournament);
            _tournamentRepository.SaveChanges();
            return tournament;

        }
    }
}