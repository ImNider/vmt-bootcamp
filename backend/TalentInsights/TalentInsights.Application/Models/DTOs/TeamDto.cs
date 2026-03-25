namespace TalentInsights.Application.Models.DTOs
{
    public class TeamDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
