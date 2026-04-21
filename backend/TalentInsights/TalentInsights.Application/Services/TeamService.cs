using System.Security.Claims;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Requests.Teams;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Domain.Database.SqlServer;

namespace TalentInsights.Application.Services
{
    public class TeamService(IUnitOfWork uow, ICollaboratorService collaboratorService) : ITeamService
    {
        public Task<GenericResponse<TeamDto>> Create(CreateTeamRequest model, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<ProjectDto>>> Get(FilterTeamRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
