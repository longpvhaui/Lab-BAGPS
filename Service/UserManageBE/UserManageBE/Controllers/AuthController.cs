using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Service.AuthenService;
using Service.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManageBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate  = authenticate;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var secretAppsettings = _configuration.GetValue<string>("AppSettings:SecretKey");
            var tokenHandler = new JwtSecurityTokenHandler();
            if (model is null)
            {
                return BadRequest("Invalid client request");
            }
            else 
            {
                var userLogin = _authenticate.CheckLogin(model);
                if (userLogin != null)
                {
                    var secretKey = Encoding.UTF8.GetBytes(secretAppsettings);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim("id", userLogin.Id.ToString()) }),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return Ok(tokenHandler.WriteToken(token));
                }
            }
            return Unauthorized();
        }
    }
}
