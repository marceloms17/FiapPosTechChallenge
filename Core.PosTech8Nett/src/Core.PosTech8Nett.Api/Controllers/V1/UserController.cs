using Asp.Versioning;
using Core.PosTech8Nett.Api.Domain.Model.User;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetUser([FromRoute] GetUserModel request, CancellationToken cancellationToken = default)
        {

            return null;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> UpdatetUser([FromBody] UpdateUserModel request, CancellationToken cancellationToken = default)
        {

            return null;
        }
    }
}
