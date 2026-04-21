using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Requests.Teams
{
    public class FilterTeamRequest
    {
        [MaxLength(150, ErrorMessage = ValidationConstants.MAX_LENGTH)]
        public string? Name { get; set; } = null;

        [MaxLength(500, ErrorMessage = ValidationConstants.MAX_LENGTH)]
        public string? Description { get; set; } = null;
    }
}
