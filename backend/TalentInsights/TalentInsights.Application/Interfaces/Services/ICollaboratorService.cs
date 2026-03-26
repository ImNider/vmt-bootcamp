using TalentInsights.Application.Models.DTOs;
using TalentInsights.Application.Models.Request.Collaborator;
using TalentInsights.Application.Models.Responses;
using TalentInsigts.Application.Models.Request.Collaborator;

namespace TalentInsights.Application.Interfaces.Services
{
    public interface ICollaboratorService
    {
        public GenericResponse<CollaboratorDto> Create(CreateCollaboratorRequest model);
        public GenericResponse<CollaboratorDto> Update(Guid collaboratorId, UpdateCollaboratorRequest model);
        public GenericResponse<List<CollaboratorDto>> GetAll(int limit, int offset);
        public GenericResponse<CollaboratorDto?> GetById(Guid collaboratorId);
        public GenericResponse<bool> Delete(Guid collaboratorId);
    }
}
