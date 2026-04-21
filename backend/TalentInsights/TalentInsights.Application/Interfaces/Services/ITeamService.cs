using System.Security.Claims;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Requests.Teams;
using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Task<GenericResponse<TeamDto>> Create(CreateTeamRequest model, Claim claim);
        Task<GenericResponse<List<ProjectDto>>> Get(FilterTeamRequest model);
    }
}
