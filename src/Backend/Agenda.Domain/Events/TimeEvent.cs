namespace Agenda.Domain.Events;

public  class TimeEvent: IDomainEvent
{
    public DateTimeOffset StartAt { get; }
    public int Duration { get; }
    public long ClientId { get; }
    public DateTimeOffset OccurredOn { get; }
    
    protected TimeEvent(DateTimeOffset startAt, int duration, long clientId) 
    {
        StartAt = startAt;
        Duration = duration;
        ClientId = clientId;
        OccurredOn = DateTimeOffset.UtcNow;
    }
}