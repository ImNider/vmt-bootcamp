using TalentInsights.Application.Helpers;
using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Collaborator;
using TalentInsights.Application.Models.Responses;
using TalentInsights.Shared.Helpers;
using TalentInsigts.Application.Models.Request.Collaborator;

namespace TalentInsights.Application.Services
{
    public class CollaboratorService : ICollaboratorService
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

            return ResponseHelper.Create(collaborator);
        }

        public GenericResponse<bool> Delete(Guid collaboratorId)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<List<CollaboratorDto>> Get(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<CollaboratorDto?> Get(Guid collaboratorId)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<CollaboratorDto> Update(Guid collaboratorId, UpdateCollaboratorRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
