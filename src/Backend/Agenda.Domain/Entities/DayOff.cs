namespace Agenda.Domain.Entities;

public sealed class DayOff : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }
    public long SchedulerId { get; set; }

    public DayOff()
    {
    }

    public DayOff(DayOfWeek dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(long id, DayOfWeek dayOnWeek)
    {
        Id = id;
        DayOnWeek = dayOnWeek;
        UpdatedAt = DateTime.UtcNow;
    }
}