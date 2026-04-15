using TalentInsights.Domain.Interfaces.Repositories;

namespace TalentInsights.Domain.Database
{
    public interface IUnitOfWork
    {
        ICollaboratorRepository collaboratorRepository { get; set; }
        Task SaveChangesAsync();
    }
}
