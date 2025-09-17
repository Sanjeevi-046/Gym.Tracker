using Gym.Tracker.Core.ServiceModel;
using Gym.Tracker.Data.Context;
using Gym.Tracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Core.Services.v1
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AuthService(IConfiguration config, ApplicationDbContext context, IUserService userService)
        {
            this._config = config;
            this._context = context;
            _userService = userService;
        }

        private SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
        }

        private async Task<AuthResponse> GetAuthResponse(User user)
        {
            List<string> permissions =await (from rt in _context.RoleTypes join rp in _context.RolePermissions on rt.Id equals rp.RoleTypeId
                                        join ap in _context.ApplicationPermissions on rp.ApplicationPermissionId equals ap.Id
                                        where rt.Id == user.RoleId select ap.PermissionName 
                                        ).ToListAsync();
            return new AuthResponse
            {
                IsSuccess = true,
                SigninName = user.Email,
                Permissions = permissions,
                UserId = (long)user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName ?? string.Empty,
                UserFullName = user.FullName ?? string.Empty,
                RoleId = (long)user.RoleId,
                Role = (await _context.RoleTypes.FirstOrDefaultAsync(_=>_.Id ==user.RoleId))?.RoleName ?? string.Empty
            };
        }

        // Custom Access Token
        public async Task<string> GenerateCustomAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("ver", "1.0"),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("username", user.FirstName),
                new Claim("signInNames.emailAddress", user.Email),
                new Claim("name", user.FullName),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim("isMFAEnabled", user.IsMailNotificationAllowed.ToString()),
                new Claim("roleId", user.RoleId.ToString()),
                new Claim("authresp",Newtonsoft.Json.JsonConvert.SerializeObject(await GetAuthResponse(user))),
                new Claim("identityProvider", "Local"),
                new Claim("isForgotPassword", user.IsForcePwdChange.ToString()),
                new Claim("isPasswordReset", user.IsForcePwdChange.ToString())
            };


            var creds = new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(15), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Refresh Token as JWT
        public string GenerateRefreshToken(string userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim("rt", "true") 
            };

            var creds = new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> AuthenticateUser(LoginRequest loginRequest)
        {
            var userVerificationResult = await _userService.IsExistingUser(loginRequest.Email!, loginRequest.Password!);
            
            if (userVerificationResult.IsPasswordInvalid && userVerificationResult.IsExistingUser)
            {
                var user = await _context.Users.FirstOrDefaultAsync(_ => _.Email.Equals(loginRequest.Email));
                return new
                {
                    Token = await GenerateCustomAccessToken(user!),
                    RefreshToken = GenerateRefreshToken(user!.Id.ToString())
                };
            }
            if (userVerificationResult.IsExistingUser && !userVerificationResult.IsPasswordInvalid) 
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            throw new UnauthorizedAccessException("No User Found");
        }
    }
}
