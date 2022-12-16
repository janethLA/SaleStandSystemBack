using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySalesStandSystem.Input;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;
using MySalesStandSystem.Repository;
using MySalesStandSystem.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MySalesStandSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);

                return Ok(new UserAuthOutput(token, user.id, user.name, user.rol));
            }
            return NotFound("Usuario no encontrado");
        }
        private User authenticate(LoginUser userLogin)
        {
            var currentUser = _userRepository.GetUsers().FirstOrDefault(
               user => user.username == userLogin.username &&
                      user.password == Encrypt.getSHA256(userLogin.password));
            if (currentUser == null)
            {
                return null;
            }
            return currentUser;

        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.username),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.GivenName, user.name),
                new Claim(ClaimTypes.Role, user.rol),
            };


            // Crear el token

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}