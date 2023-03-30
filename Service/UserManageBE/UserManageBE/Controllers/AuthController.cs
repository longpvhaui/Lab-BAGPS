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

        /// <summary>Logins the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
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
                        Subject = new ClaimsIdentity(new[] { new Claim("id",userLogin.Id.ToString()) }),
                        Expires = DateTime.UtcNow.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenResponse = tokenHandler.WriteToken(token) ;
                    var response = new ResponseAuthen();
                    response.User = userLogin;
                    response.Token = tokenResponse;
                    return Ok(response);
                }else
                {
                    return Ok("Username or Password is incorrect");
                }
            }   
 
        }
    }
}
