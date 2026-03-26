using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Skill;
using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Interfaces.Services
{
    public interface ISkillService
    {
        public GenericResponse<SkillDto> Create(CreateSkillRequest model);
        public GenericResponse<List<SkillDto>> GetAll(int limit, int offset);
        public GenericResponse<SkillDto?> GetById(Guid id);
        public GenericResponse<bool> Delete(Guid id);
    }
}
