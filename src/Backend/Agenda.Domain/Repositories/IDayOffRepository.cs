using Agenda.Domain.Entities;

namespace Agenda.Domain.Repositories;

public interface IDayOffRepository
{
    Task AddRangeAsync(IList<DayOff> dayOffs);
}