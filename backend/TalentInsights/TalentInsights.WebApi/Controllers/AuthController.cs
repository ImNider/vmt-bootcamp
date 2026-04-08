using Microsoft.AspNetCore.Mvc;
using TalentInsights.Application.Interfaces;
using TalentInsights.Application.Models.Requests.Auth;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Application.Models.Responses.Auth;

namespace TalentInsights.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("login")]
        [EndpointSummary("Iniciar sesión como colaborador")]
        [EndpointDescription("Esto le permite iniciar sesión en el aplicativo. Genera dos tokens, uno que es el JWT para la autenticación en el aplicativo y el otro que es el que le permite realizar la renovación")]
        [ProducesResponseType<GenericResponse<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GenericResponse<LoginAuthResponse>>(StatusCodes.Status200OK)]
        [Tags("auth", "collaborators", "JWT")]
        public async Task<IActionResult> Login([FromBody] LoginAuthRequest model)
        {
            var srv = await service.Login(model);
            return Ok(srv);
        }

        [HttpPost("renew")]
        [EndpointSummary("Renovar la sesión del colaborador")]
        [EndpointDescription("Esto le permite renovar sesión en el aplicativo. Genera dos tokens, uno que es el JWT para la autenticación en el aplicativo y el otro que es el que le permite realizar la renovación")]
        [ProducesResponseType<GenericResponse<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GenericResponse<RenewAuthRequest>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Renew([FromBody] RenewAuthRequest model)
        {
            var srv = await service.Renew(model);
            return Ok(srv);
        }
    }
}
