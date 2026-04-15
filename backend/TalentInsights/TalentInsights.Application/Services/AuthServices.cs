using Microsoft.Extensions.Configuration;
using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.Helpers;
using TalentInsights.Application.Models.Requests.Auth;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Application.Models.Responses.Auth;
using TalentInsights.Domain.Database;
using TalentInsights.Domain.Exceptions;
using TalentInsights.Shared;
using TalentInsights.Shared.Constants;

namespace TalentInsights.Application.Services
{
    public class AuthServices(IUnitOfWork uow, IConfiguration configuration, ICacheService cacheService) : IAuthService
    {
        public async Task<GenericResponse<LoginAuthResponse>> Login(LoginAuthRequest model)
        {
            var collaborator = await uow.collaboratorRepository.Get(model.Email)
                ?? throw new BadRequestException("Usuario o contraseña incorrectos");

            var validatePassword = Hasher.ComparePassword(model.Password, collaborator.Password);
            if (!validatePassword)
            {
                throw new BadRequestException("Usuario o contraseña incorrectos");
            }

            var token = TokenHelper.Create(collaborator.Id, [.. collaborator.CollaboratorRoleCollaborators.Select(x => x.Role.Name)], configuration, cacheService);
            var refreshToken = TokenHelper.CreateRefresh(collaborator.Id, configuration, cacheService);

            return ResponseHelper.Create(new LoginAuthResponse
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }

        public async Task<GenericResponse<LoginAuthResponse>> Renew(RenewAuthRequest model)
        {
            var findRefreshToken = cacheService.Get<RefreshToken>(CacheHelper.AuthRefreshTokenKey(model.RefreshToken))
                ?? throw new NotFoundException("El token para refrescar la sesión expiró, no existe o es incorrecto");

            var collaborator = await uow.collaboratorRepository.Get(findRefreshToken.CollaboratorId)
                ?? throw new NotFoundException(ResponseConstants.COLLABORATOR_NOT_EXISTS);

            var token = TokenHelper.Create(collaborator.Id, [.. collaborator.CollaboratorRoleCollaborators.Select(x => x.Role.Name)], configuration, cacheService);
            var refreshToken = TokenHelper.CreateRefresh(findRefreshToken.CollaboratorId, configuration, cacheService);

            cacheService.Delete(CacheHelper.AuthRefreshTokenKey(model.RefreshToken));

            return ResponseHelper.Create(new LoginAuthResponse
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }
    }
}
