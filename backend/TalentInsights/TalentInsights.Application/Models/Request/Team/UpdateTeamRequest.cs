using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Request.Team
{
    public class UpdateTeamRequest
    {
        [MaxLength(150, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        public string? Name { get; set; }

        [MaxLength(500, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        public string? Description { get; set; }

        public bool? IsPublic { get; set; }
    }
}
