using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Collaborator;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared;
using TalentInsights.Shared.Helpers;
using TalentInsigts.Application.Models.Request.Collaborator;

namespace TalentInsights.Application.Services
{
    public class CollaboratorService(Cache<CollaboratorDto> _cache) : ICollaboratorService
    {
        public GenericResponse<CollaboratorDto> Create(CreateCollaboratorRequest model)
        {
            var collaborator = new CollaboratorDto
            {
                CollaboratorId = Guid.NewGuid(),
                FullName = model.FullName,
                GitlabProfile = model.GitlabProfile,
                Position = model.Position,
                CreatedAt = DateTimeHelper.UtcNow(),
                JoinedAt = DateTimeHelper.UtcNow(),
            };
            _cache.Add(collaborator.CollaboratorId.ToString(), collaborator);
            return ResponseHelper.Create(collaborator);
        }

        public GenericResponse<bool> Delete(Guid id)
        {
            var exist = _cache.Get(id.ToString());
            if (exist is null)
            {
                return ResponseHelper.Create(false);
            }
            _cache.Delete(id.ToString());
            return ResponseHelper.Create(true);
        }

        public GenericResponse<List<CollaboratorDto>> GetAll(int limit, int offset)
        {
            var collaborators = _cache.Get();
            return ResponseHelper.Create(collaborators);
        }

        public GenericResponse<CollaboratorDto?> GetById(Guid id)
        {
            var collaborator = _cache.Get(id.ToString());
            return ResponseHelper.Create(collaborator);
        }

        public GenericResponse<CollaboratorDto> Update(Guid collaboratorId, UpdateCollaboratorRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
