using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Skill;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared;

namespace TalentInsights.Application.Services
{
    public class SkillService(Cache<SkillDto> _cache) : ISkillService
    {
        public GenericResponse<SkillDto> Create(CreateSkillRequest model)
        {
            var skill = new SkillDto
            {
                SkillId = Guid.NewGuid(),
                Name = model.Name,
                Category = model.Category,

            };
            _cache.Add(skill.SkillId.ToString(), skill);
            return ResponseHelper.Create(skill);
        }

        public GenericResponse<List<SkillDto>> GetAll(int limit, int offset)
        {
            var skills = _cache.Get();
            return ResponseHelper.Create(skills);
        }

        public GenericResponse<SkillDto?> GetById(Guid id)
        {
            var skill = _cache.Get(id.ToString());
            return ResponseHelper.Create(skill);
        }

        public GenericResponse<bool> Delete(Guid id)
        {
            var exist = _cache.Get(id.ToString());
            if (exist is null)
            {
                return ResponseHelper.Create(false);
            }
            _cache.Delete(id.ToString());
            return ResponseHelper.Create(true);
        }
    }
}
