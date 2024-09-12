namespace Agenda.Domain.Events;

public class BusinessHourReservedEvent : IDomainEvent
{
    public DateTimeOffset StartAt { get; }
    public int Duration { get; }
    public long ClientId { get; }
    public DateTimeOffset OccurredOn { get; }

    internal BusinessHourReservedEvent(DateTimeOffset startAt, int duration, long clientId)
    {
        StartAt = startAt;
        Duration = duration;
        ClientId = clientId;
        OccurredOn = DateTimeOffset.Now;
    }
}