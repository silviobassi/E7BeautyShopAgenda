using Agenda.Domain.Repositories;

namespace Agenda.Infrastructure.DataAccess;

public class UnitOfWork(AgendaDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync() => await dbContext.SaveChangesAsync();

    public void AutoDetectChangesEnabled(bool enabled) => dbContext.ChangeTracker.AutoDetectChangesEnabled = enabled;
    
}