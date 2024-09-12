namespace Agenda.Domain.Entities;

public class BusinessHour
    : Entity
{
    public DateTimeOffset StartAt { get; private set; }
    public DateTimeOffset EndAt { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }

    public BusinessHour(DateTimeOffset startAt, DateTimeOffset endAt, int duration)
    {
        StartAt = startAt;
        EndAt = endAt;
        Active = true;
        Available = true;
        Duration = duration;
        CreatedAt = DateTimeOffset.Now;
    }

    public void Update(long id, DateTimeOffset newStartAt, DateTimeOffset newEndAt,
        int newDuration)
    {
        Id = id;
        StartAt = newStartAt;
        EndAt = newEndAt;
        Duration = newDuration;
        UpdatedAt = DateTimeOffset.Now;
    }

    public void ToggleAvailability() => Available = !Available;
}