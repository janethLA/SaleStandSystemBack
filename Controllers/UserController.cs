using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;
using System.Data;

namespace MySalesStandSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        [ActionName(nameof(GetUsersAsync))]
        public IEnumerable<User> GetUsersAsync()
        {
            return _userRepository.GetUsers();
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetUserById))]
        public ActionResult<User> GetUserById(int id)
        {
            var userByID = _userRepository.GetUserById(id);
            if (userByID == null)
            {
                return NotFound();
            }
            return userByID;
        }

        [HttpPost]
        [ActionName(nameof(CreateUserAsync))]
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(user);

            return NoContent();
        }


        [HttpGet("/api/saleStandsByUser/{id}")]
        [Authorize(Roles = ("seller"))]
        [ActionName(nameof(getSaleStandsByUser))]
        public List<SalesStandOutput> getSaleStandsByUser(int id)
        {
            return _userRepository.getSaleStandsByUser(id);
        }
    }
}
