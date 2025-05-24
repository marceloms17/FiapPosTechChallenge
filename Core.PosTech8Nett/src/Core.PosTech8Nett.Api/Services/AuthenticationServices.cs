using Core.PosTech8Nett.Api.CommonExtensions;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Domain.Validations.Authenticator;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthenticationServices(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var resultValidate = new LoginRequestValidator().Validate(request);

            if (resultValidate.IsValid is false)
            {
                var messages = string.Concat("Message is invalid, validation errors: ", resultValidate.Errors.ConvertToString());
                throw new Exception(messages);
            }

            var userData = await _userRepository.GetByEmailAsync(request.Email);

            if (userData != null && await _userRepository.CheckPasswordAsync(userData, request.Password))
            {
                var token = await GenerateJwtToken(userData);
                return token;
            }

            return null;
        }

        private async Task<string> GenerateJwtToken(UsersEntitie user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Adicionar todas as roles como claims
           // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
