using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Request.Team
{
    public class CreateTeamRequest
    {
        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(150, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        public string Name { get; set; } = null!;

        [MaxLength(500, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        public string? Description { get; set; }

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        public bool IsPublic { get; set; }

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        public Guid CreatedBy { get; set; }
    }
}
