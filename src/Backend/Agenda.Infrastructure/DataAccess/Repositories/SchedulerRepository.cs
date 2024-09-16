using Agenda.Application.Commands.Scheduler;
using Agenda.Domain.Entities;

namespace Agenda.Infrastructure.DataAccess.Repositories;

public class SchedulerRepository(AgendaDbContext dbContext) : ISchedulerRepository
{
    public async Task CreateAsync(Scheduler scheduler) => await dbContext.Schedulers.AddAsync(scheduler);
}