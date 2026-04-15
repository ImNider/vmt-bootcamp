using TalentInsights.Domain.Database;
using TalentInsights.Domain.Database.SqlServer.Context;
using TalentInsights.Domain.Interfaces.Repositories;

namespace TalentInsights.Infrastructure.Persistence.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalentInsightsContext _context;
        public ICollaboratorRepository collaboratorRepository { get; set; }

        public UnitOfWork(TalentInsightsContext context, ICollaboratorRepository collaboratorsRepository)
        {
            collaboratorRepository = collaboratorsRepository;
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
