using Asp.Versioning;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            await _userServices.CreateAsync(request);

            return Ok(new { message = "Usuário registrado!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationServices.LoginAsync(request);

            if (result is not null)
                return Ok(result);

            return Unauthorized();
        }
    }
}