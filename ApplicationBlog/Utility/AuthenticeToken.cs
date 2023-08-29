using ApplicationBlog.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApplicationBlog.Utility
{
    public class AuthenticeToken
    {
        private IConfiguration _config;
        public AuthenticeToken(IConfiguration config)
        {
            _config = config;
        }
        //public string? ValidateToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var Username = jwtToken.Claims.First(x => x.Type == "Username").Value;

        //        // return user id from JWT token if validation successful
        //        return Username;
        //    }
        //    catch
        //    {
        //        // return null if validation fails
        //        return null;
        //    }
        //}


    }
}
