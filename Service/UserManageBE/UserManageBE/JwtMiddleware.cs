
using Microsoft.IdentityModel.Tokens;
using Service.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UserManageBE
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// longpv 3/30/2023 created
    /// </Modified>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>Invokes the specified context.</summary>
        /// <param name="context">The context.</param>
        /// <param name="userService">The user service.</param>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            if (token != null)
                AttachUserToContext(context, userService, token);

            await _next(context);
        }


        /// <summary>Attaches the user to context.</summary>
        /// <param name="context">The context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="token">The token.</param>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
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
