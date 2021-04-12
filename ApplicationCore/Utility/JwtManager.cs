using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Utility
{
    public static class JwtManager
    {
        public static string GenerateToken(ClaimsIdentity claims, string jwtSecretkey, 
            DateTime? expiryToken)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(jwtSecretkey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (expiryToken != null) tokenDescriptor.Expires = Convert.ToDateTime(expiryToken);

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token, string jwtSecretKey, bool requireExpiryTime)
        {
            var claims = new ClaimsPrincipal();

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    IdentityModelEventSource.ShowPII = true;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadToken(token) as SecurityToken;

                    if (jwtToken != null)
                    {
                        var symmetricKey = Encoding.ASCII.GetBytes(jwtSecretKey);

                        var validationParameters = new TokenValidationParameters()
                        {
                            RequireExpirationTime = requireExpiryTime,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                        };

                        claims = tokenHandler.ValidateToken(token, validationParameters, out jwtToken);
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return claims;
        }
    }
}
