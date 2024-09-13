namespace Agenda.Domain.Events;

public class TimeCanceledEvent(DateTimeOffset startAt, int duration, long clientId, string reason) : TimeEvent(startAt,
    duration, clientId)
{
    public string Reason { get; private set; } = reason;
}