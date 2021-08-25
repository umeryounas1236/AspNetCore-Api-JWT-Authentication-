using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNet_Core_API__JWT_Authentication_.JwtService
{
    public class JWTHandler : IJwtHandler
    {
        private readonly JWTSettings jwtSettings;

        public JWTHandler(IOptions<JWTSettings> _jwtSettings)
        {
            jwtSettings = _jwtSettings.Value;
        }


        public string GenerateJWTToken(string userName)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            byte[] secret = Encoding.ASCII.GetBytes(jwtSettings.Key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName)
                    }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secret),
            SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public string ValidateJWTToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var username = jwtToken.Claims.First(x => x.Type == "unique_name")?.Value;

                return username;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
