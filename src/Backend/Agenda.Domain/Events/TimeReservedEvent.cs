namespace Agenda.Domain.Events;

public class TimeReservedEvent(DateTimeOffset startAt, int duration, long clientId)
    : TimeEvent(startAt, duration, clientId);