using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Request.Collaborator
{
    public class CreateCollaboratorRequest
    {
        [Required]
        [MaxLength(150, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(10, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public required string FullName { get; set; }
        public string? GitlabProfile { get; set; }
        [Required]
        public required string Position { get; set; }
    }
}
