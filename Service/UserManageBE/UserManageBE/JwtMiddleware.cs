
using Microsoft.IdentityModel.Tokens;
using Service.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UserManageBE
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            if (token != null)
                AttachUserToContext(context, userService, token);

            await _next(context);
        }


        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandle = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:SecretKey"));
                tokenHandle.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken) ;
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId =int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);


                context.Items["User"] = userService.GetUser(userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
