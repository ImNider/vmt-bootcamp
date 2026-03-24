using Microsoft.AspNetCore.Mvc;
using YoutubeClone.Application.Models.Request.Users;

namespace YoutubeClone.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest model)
        {
            return Ok($"¡Usuario {model.Username} creado exitosamente!");
        }
    }
}
