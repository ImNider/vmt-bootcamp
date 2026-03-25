using Microsoft.AspNetCore.Mvc;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.Request.Team;

namespace TalentInsights.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService teamService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamRequest model)
        {
            var rsp = teamService.Create(model);
            return Ok(rsp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rsp = teamService.GetAll();
            return Ok(rsp);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rsp = teamService.GetById(id);
            return Ok(rsp);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rsp = teamService.Delete(id);
            return Ok(rsp);
        }
    }
}