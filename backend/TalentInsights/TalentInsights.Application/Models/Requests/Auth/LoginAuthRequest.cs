using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Models.Requests.Auth
{
    public class LoginAuthRequest
    {
        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [EmailAddress(ErrorMessage = ValidationConstants.EMAIL_ADDRESS)]
        [Description("El correo del colaborador")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = ValidationConstants.REQUIRED)]
        [Description("La contraseña del colaborador")]
        public string Password { get; set; } = null!;
    }
}
