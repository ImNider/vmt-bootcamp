using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Requests.Projects;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Domain.Exceptions;
using TalentInsights.Shared.Constants;
using TalentInsights.WebApi.Helpers;

namespace TalentInsights.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService service) : ControllerBase
    {
        [HttpPost]
        public async Task<GenericResponse<ProjectDto>> Create([FromBody] CreateProjectRequest model)
        {
            var srv = await service.Create(model, UserClaim());
            return ResponseStatus.Created(HttpContext, srv);
        }

        private Claim UserClaim()
        {
            return User.FindFirst(ClaimsConstants.COLLABORATOR_ID)
                ?? throw new BadRequestException(ResponseConstants.AUTH_CLAIM_USER_NOT_FOUND);
        }
    }
}
