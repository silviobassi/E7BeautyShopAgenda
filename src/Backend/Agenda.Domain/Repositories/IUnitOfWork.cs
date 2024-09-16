namespace Agenda.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
    void AutoDetectChangesEnabled(bool enabled);
}
