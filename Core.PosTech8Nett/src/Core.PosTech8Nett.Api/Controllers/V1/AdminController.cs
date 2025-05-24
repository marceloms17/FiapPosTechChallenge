using Asp.Versioning;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Core.PosTech8Nett.Api.Controllers.V1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AdminController : ControllerBase
    {

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch("Block")]
        public async Task<IActionResult> BlockUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
        {

            return null;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
        {

            return null;
        }
    }
}
