using Asp.Versioning;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Controllers.V1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail([FromQuery] GetUserByEmailRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userServices.GetByEmailAsync(request);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetByNickName")]
        public async Task<IActionResult> GetNickName([FromQuery] GetUserByNickNameRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userServices.GetByNickNameAsync(request);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetUserByIdRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userServices.GetByIdAsync(request);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("List")]
        public async Task<IActionResult> List([FromQuery] GetUserByEmailRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userServices.GetByEmailAsync(request);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch("Update")]
        public async Task<IActionResult> UpdatetUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
        {

            return null;
        }
    }
}
