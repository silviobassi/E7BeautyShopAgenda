namespace Agenda.Domain.Entities;

public sealed class DayRest : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }

    public long AgendaId { get; private set; }

    public DayRest()
    {
    }

    public DayRest(DayOfWeek dayOnWeek, long agendaId)
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        DayOnWeek = dayOnWeek;
        AgendaId = agendaId;
    }

    public void Update(long id, DayOfWeek dayOnWeek, long agendaId)
    {
        Id = id;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        DayOnWeek = dayOnWeek;
        AgendaId = agendaId;
    }
}