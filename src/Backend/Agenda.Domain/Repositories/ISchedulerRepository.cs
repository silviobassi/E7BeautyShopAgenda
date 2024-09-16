namespace Agenda.Application.Commands.Scheduler;

public interface ISchedulerRepository
{
    Task CreateAsync(Domain.Entities.Scheduler scheduler);
}