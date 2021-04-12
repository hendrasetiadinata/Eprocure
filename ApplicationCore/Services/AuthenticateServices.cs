using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class AuthenticateServices : IAuthenticate
    {
        private readonly IUser _user;
        private readonly ConfigOptions _options;
        private readonly IJwtManager _jwtManager;

        public AuthenticateServices(IUser user, ConfigOptions options, IJwtManager jwtManager)
        {
            _user = user;
            _options = options;
            _jwtManager = jwtManager;
        }

        public async Task<AuthenticateResponse> GetTokenAsync(AuthenticateRequest request)
        {
            var userData = await _user.GetUserByLogin(request.Username, request.Password);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userData.UserId),
                new Claim(ClaimTypes.Role, userData.Role)
            };
            var claimIdentity = new ClaimsIdentity(claims);
            var token = _jwtManager.GenerateToken(claimIdentity, _options.JwtOptions);

            if (string.IsNullOrEmpty(token)) throw new Exception("Something went wrong in get token.");

            return new AuthenticateResponse()
            {
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Token = token
            };
        }
    }
}
