using TalentInsights.Application.Models.Requests.Auth;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Application.Models.Responses.Auth;

namespace TalentInsights.Application.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<LoginAuthResponse>> Login(LoginAuthRequest model);
        Task<GenericResponse<LoginAuthResponse>> Renew(RenewAuthRequest model);
    }
}
