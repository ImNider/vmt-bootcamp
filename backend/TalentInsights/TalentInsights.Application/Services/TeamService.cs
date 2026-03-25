
using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Team;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared;
using TalentInsights.Shared.Helpers;

namespace TalentInsights.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly Cache<TeamDto> _cache;

        public TeamService(Cache<TeamDto> cache)
        {
            _cache = cache;
        }

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
            bool isDeleted = _cache.Delete(id.ToString());
            // validar
            return ResponseHelper.Create(isDeleted);
        }

        public GenericResponse<List<TeamDto>> GetAll()
        {
            var teams = _cache.Get();
            return ResponseHelper.Create(teams);
        }

        public GenericResponse<TeamDto> GetById(Guid id)
        {
            var team = _cache.Get(id.ToString());
            return ResponseHelper.Create(team);
        }
    }
}
