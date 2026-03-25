using TalentInsights.Application.Helpers;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Skill;
using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Services
{
    public class SkillService
    {
        public GenericResponse<SkillDto> Create(CreateSkillRequest model)
        {
            var skill = new SkillDto
            {
                Name = model.Name,
            };

            return ResponseHelper.Create(skill);
        }
    }
}
