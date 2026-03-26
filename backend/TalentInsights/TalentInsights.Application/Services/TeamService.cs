
using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Team;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared;
using TalentInsights.Shared.Helpers;

namespace TalentInsights.Application.Services
{
    public class TeamService(Cache<TeamDto> _cache) : ITeamService
    {
        public GenericResponse<TeamDto> Create(CreateTeamRequest model)
        {
            var team = new TeamDto
            {
                TeamId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                IsPublic = model.IsPublic,
                CreatedBy = model.CreatedBy,
                CreatedAt = DateTimeHelper.UtcNow(),
                UpdatedAt = DateTimeHelper.UtcNow()
            };
            _cache.Add(team.TeamId.ToString(), team);
            return ResponseHelper.Create(team);
        }

        public GenericResponse<bool> Delete(Guid id)
        {
            var exist = _cache.Get(id.ToString());
            if (exist == null)
            {
                return ResponseHelper.Create(false);
            }
            _cache.Delete(id.ToString());
            return ResponseHelper.Create(true);
        }

        public GenericResponse<List<TeamDto>> GetAll(int limit, int offset)
        {
            var teams = _cache.Get();
            return ResponseHelper.Create(teams);
        }

        public GenericResponse<TeamDto?> GetById(Guid id)
        {
            var team = _cache.Get(id.ToString());
            return ResponseHelper.Create(team);
        }
    }
}
