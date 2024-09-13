namespace Agenda.Domain.Events;

public class TimeReservedEvent : TimeEvent
{
    internal TimeReservedEvent(DateTimeOffset startAt, int duration, long clientId) : base(startAt, duration, clientId)
    {
    }
    
}