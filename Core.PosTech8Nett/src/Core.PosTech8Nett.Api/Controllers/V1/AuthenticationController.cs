using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;

namespace Core.PosTech8Nett.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(IUserServices userServices, IAuthenticationServices authenticationServices)
        {
            _userServices = userServices;
            _authenticationServices = authenticationServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            await _userServices.CreateAsync(request);

            return Ok(new { message = "Usuário registrado!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationServices.LoginAsync(request);

            if(result is not null)
                return Ok(result);

            return Unauthorized();
        }

      
    }


}