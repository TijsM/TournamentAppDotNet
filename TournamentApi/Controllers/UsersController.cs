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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> getUser(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(getUser), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();

            _userRepository.update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            User user = _userRepository.GetById(id);
            if(user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return user;
        }

        

    }
}