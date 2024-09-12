namespace Agenda.Domain.Entities;

public sealed class DayRest : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }

    public DayRest()
    {
    }

    public DayRest(DayOfWeek dayOnWeek)
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