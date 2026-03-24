using Microsoft.AspNetCore.Mvc;

namespace TalentInsights.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok("Usuario creado");
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok($"{id}");
        }

        /*[HttpPost]
        public async Task<IActionResult> GetAll()
        {
            return Ok("Todos los usuarios");
        }

        [HttpPost]
        public async Task<IActionResult> Update()
        {
            return Ok("Usuario actualizado");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok("Contraeña cambiada");
        }*/
    }
}
