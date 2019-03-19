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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository repo)
        {
            _userRepository = repo;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>list of users</returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }


        /// <summary>
        /// Get the user with the given id
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>The User</returns>
        [HttpGet("{id}")]
        public ActionResult<User> getUser(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">the user that has to be created</param>
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(getUser), new { id = user.UserId }, user);
        }


        /// <summary>
        /// Modifies the User
        /// </summary>
        /// <param name="id">id of the user that has to be modified</param>
        /// <param name="user">the modified object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();

            _userRepository.update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// removes a user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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