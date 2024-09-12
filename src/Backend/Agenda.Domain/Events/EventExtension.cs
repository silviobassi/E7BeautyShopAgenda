using Agenda.Domain.Entities;

namespace Agenda.Domain.Events;

public static class EventExtension
{
    public static BusinessHourReservedEvent CreateBusinessHourReservedEvent(this BusinessHour businessHour,
        DateTimeOffset startAt, int duration,
        long clientId)
    {
        return new BusinessHourReservedEvent(startAt, duration, clientId);
    }
}