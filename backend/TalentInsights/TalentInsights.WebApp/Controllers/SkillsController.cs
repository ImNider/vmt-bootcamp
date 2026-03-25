using Microsoft.AspNetCore.Mvc;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.Request.Skill;

namespace TalentInsights.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController(ISkillService skillService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkillRequest model)
        {
            var rsp = skillService.Create(model);
            return Ok(rsp);
        }
    }
}
