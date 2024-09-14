using Agenda.Domain.ValueObjects;

namespace Agenda.Domain.Entities;

public class Scheduler(IEnumerable<DayOff> daysOff, Weekend weekend, Weekday weekday, long professionalId) : Entity
{
    public IEnumerable<DayOff> DaysOff { get; private set; } = daysOff;
    public Weekend Weekend { get; private set; } = weekend;
    public Weekday Weekday { get; private set; } = weekday;
    public long ProfessionalId { get; private set; } = professionalId;
}