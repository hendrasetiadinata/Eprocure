using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Jwt
{
    public class JwtManager: IJwtManager
    {
        public JwtManager()
        {
        }

        public string GenerateToken(ClaimsIdentity claimsIdentity, JwtOptions options)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(options.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var expiryTime = DateTime.Now.AddMinutes(Convert.ToInt32(options.Expires));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expiryTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }

        public ClaimsPrincipal GetPrincipal(JwtOptions options, string token, bool requireExpTime)
        {
            IdentityModelEventSource.ShowPII = true;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken jwtToken = tokenHandler.ReadToken(token);

            if (jwtToken != null)
            {
                var symmetricKey = Encoding.ASCII.GetBytes(options.SecretKey);
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = requireExpTime,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                return tokenHandler.ValidateToken(token, validationParameters, out jwtToken);
            }

            return null;
        }
    }
}
