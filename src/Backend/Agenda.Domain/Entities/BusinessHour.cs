namespace Agenda.Domain.Entities;

public class BusinessHour
    : Entity
{
    public DateTimeOffset StartAt { get; private set; }
    public DateTimeOffset EndAt { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ProfessionalId { get; private set; }

    public BusinessHour(DateTimeOffset startAt, DateTimeOffset endAt, int duration, long professionalId)
    {
        StartAt = startAt;
        EndAt = endAt;
        Active = true;
        Available = true;
        Duration = duration;
        ProfessionalId = professionalId;
        CreatedAt = DateTimeOffset.Now;
    }

    public void Update(long id, DateTimeOffset newStartAt, DateTimeOffset newEndAt,
        int newDuration, long professionalId)
    {
        Id = id;
        StartAt = newStartAt;
        EndAt = newEndAt;
        Duration = newDuration;
        ProfessionalId = professionalId;
        UpdatedAt = DateTimeOffset.Now;
    }

    public void ToggleAvailability() => Available = !Available;
}