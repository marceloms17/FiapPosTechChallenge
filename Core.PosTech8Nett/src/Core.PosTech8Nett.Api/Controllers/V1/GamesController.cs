using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Core.PosTech8Nett.Api.Controllers.V1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class GamesController : ControllerBase
    {
    }
}
