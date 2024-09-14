using Agenda.Domain.ValueObjects;

namespace Agenda.Domain.Entities;

public class Scheduler : Entity
{
    public IEnumerable<DayOff> DaysOff { get; private set; }
    public Weekend Weekend { get; private set; }
    public Weekday Weekday { get; private set; }
    public long ProfessionalId { get; private set; }

    public Scheduler(IEnumerable<DayOff> daysOff, Weekend weekend, Weekday weekday, long professionalId)
    {
        DaysOff = daysOff;
        Weekend = weekend;
        Weekday = weekday;
        ProfessionalId = professionalId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, List<DayOff> newDaysOff, Weekend newWeekend, Weekday newWeekday, long newProfessionalId)
    {
        Id = id;
        DaysOff = newDaysOff;
        Weekend = newWeekend;
        Weekday = newWeekday;
        ProfessionalId = newProfessionalId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}