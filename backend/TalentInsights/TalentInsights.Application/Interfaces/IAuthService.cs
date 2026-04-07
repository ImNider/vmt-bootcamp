using TalentInsights.Application.Models.Requests.Auth;
using TalentInsights.Application.Models.Responses;

namespace TalentInsights.Application.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<string>> Login(LoginAuthRequest model);
    }
}
