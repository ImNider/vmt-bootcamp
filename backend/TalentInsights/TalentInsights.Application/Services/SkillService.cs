using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Skill;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared;

namespace TalentInsights.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly Cache<SkillDto> _cache;

        public SkillService(Cache<SkillDto> cache)
        {
            _cache = cache;
        }

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

        public GenericResponse<List<SkillDto>> GetAll()
        {
            var skills = _cache.Get();
            return ResponseHelper.Create(skills);
        }

        public GenericResponse<SkillDto> GetById(Guid id)
        {
            var skill = _cache.Get(id.ToString());
            // Aquí se validaría, SI SUPIERA COMO
            return ResponseHelper.Create(skill);
        }

        public GenericResponse<bool> Delete(Guid id)
        {
            bool isDeleted = _cache.Delete(id.ToString());
            // Aquí se validaría, SI SUPIERA COMO
            return ResponseHelper.Create(isDeleted);
        }
    }
}
