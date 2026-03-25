using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Request.Skill
{
    public class CreateSkillRequest
    {
        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        public string Name { get; set; } = null!;
    }
}
