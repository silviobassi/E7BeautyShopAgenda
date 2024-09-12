namespace Agenda.Domain.Entities;

public class BusinessHour
    : Entity
{
    public DateTimeOffset StartAt { get; private set; }
    public DateTimeOffset EndAt { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public BusinessHour(DateTimeOffset startAt, DateTimeOffset endAt, bool active, bool available, int duration)
    {
        StartAt = startAt;
        EndAt = endAt;
        Active = active;
        Available = available;
        Duration = duration;
        CreatedAt = DateTimeOffset.Now;
    }

    public void Update(long id, DateTimeOffset newStartAt, DateTimeOffset newEndAt, bool newActive, bool newAvailable,
        int newDuration)
    {
        Id = id;
        StartAt = newStartAt;
        EndAt = newEndAt;
        Active = newActive;
        Available = newAvailable;
        Duration = newDuration;
        UpdatedAt = DateTimeOffset.Now;
    }
}