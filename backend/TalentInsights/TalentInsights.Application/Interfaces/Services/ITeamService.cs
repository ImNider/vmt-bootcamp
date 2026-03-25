using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Team;
using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Interfaces.Services
{
    public interface ITeamService
    {
        public GenericResponse<TeamDto> Create(CreateTeamRequest model);
        public GenericResponse<List<TeamDto>> GetAll();
        public GenericResponse<TeamDto> GetById(Guid id);
        public GenericResponse<bool> Delete(Guid id);
    }
}
