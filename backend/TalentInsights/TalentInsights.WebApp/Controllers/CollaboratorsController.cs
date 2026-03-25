using Microsoft.AspNetCore.Mvc;
using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.Request.Collaborator;
using TalentInsigts.Application.Models.Request.Collaborator;

namespace TalentInsights.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorsController(ICollaboratorService collaboratorService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCollaboratorRequest model)
        {
            var rsp = collaboratorService.Create(model);
            return Ok(rsp);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateCollaboratorRequest model, Guid id)
        {
            return Ok($"Usuario actualizado: {id} - {model.FullName}");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok($"Usuario eliminado: {id}");
        }

        [HttpPatch("change-password/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordCollaboratorRequest model)
        {
            return Ok($"Contraseña cambiada: {model.OldPassword} - {model.NewPassword}");
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCollaboratorRequest model)
        {
            return Ok(ResponseHelper.Create($"Todos los usuarios: limit: {model.Limit}, offset: {model.Offset}, gitlab: {model.GitlabProfile}"));
        }*/

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCollaboratorRequest model)
        {
            List<string> users = ["Usuario 1", "Usuario 2", "Usuario 3"];
            return Ok(ResponseHelper.Create(users));
        }
    }
}
