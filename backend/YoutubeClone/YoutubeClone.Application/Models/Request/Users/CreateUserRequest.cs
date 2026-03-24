using System.ComponentModel.DataAnnotations;
using YoutubeClone.Shared.Constants;

namespace YoutubeClone.Application.Models.Request.Users
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(30, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(5, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(50, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(10, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public string DisplayName { get; set; } = null!;

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(60, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(10, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(60, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(10, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public DateOnly Bithday { get; set; }

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(30, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(5, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [MaxLength(30, ErrorMessage = ValidationConstants.MAX_LENGHT)]
        [MinLength(5, ErrorMessage = ValidationConstants.MIN_LENGHT)]
        public string Password { get; set; } = null!;
    }
}
