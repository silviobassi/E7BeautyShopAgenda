namespace Agenda.Domain.Events;

public class TimeCanceledEvent : TimeEvent
{
    public string Reason { get; private set; }

    internal TimeCanceledEvent(DateTimeOffset startAt, int duration, long clientId, string reason) : base(startAt,
        duration, clientId) => Reason = reason;
}