using Agenda.Domain.Entities;
using Agenda.Domain.Repositories;

namespace Agenda.Infrastructure.DataAccess.Repositories;

public class DayOffRepository(AgendaDbContext dbContext) : IDayOffRepository
{
    public async Task AddRangeAsync(IList<DayOff> dayOffs) => await dbContext.DaysOff.AddRangeAsync(dayOffs);
}