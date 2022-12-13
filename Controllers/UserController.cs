using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;
using MySalesStandSystem.Repository;

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

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateUser))]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            var c = _userRepository.GetUserById(id);
            if (c == null)
            {
                return NotFound();
            }

            if (!user.name.IsNullOrEmpty()) {
                Console.WriteLine("**********************8"+user.name);
                c.name = user.name;
            }
            if (!user.username.IsNullOrEmpty())
            {
                c.username = user.username;
            }
            if (!user.email.IsNullOrEmpty())
            {
                c.email = user.email;
            }
          
            await _userRepository.UpdateUserAsync(c);
            return NoContent();

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
        [ActionName(nameof(getSaleStandsByUser))]
        public List<SalesStandOutput> getSaleStandsByUser(int id)
        {
            return _userRepository.getSaleStandsByUser(id);
        }
    }
}
