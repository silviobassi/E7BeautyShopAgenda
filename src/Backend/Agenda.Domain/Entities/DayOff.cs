namespace Agenda.Domain.Entities;

public sealed class DayOff : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }

    public DayOff()
    {
    }

    public DayOff(DayOfWeek dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
        CreatedAt = DateTime.Now;
    }

    public void Update(long id, DayOfWeek dayOnWeek)
    {
        Id = id;
        DayOnWeek = dayOnWeek;
        UpdatedAt = DateTime.Now;
    }
}