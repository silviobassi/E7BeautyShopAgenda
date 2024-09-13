using Agenda.Domain.Entities;

namespace Agenda.Domain.Events;

public static class EventExtension
{
    public static T EmitReservedTimeEvent<T>(this BusinessHour businessHour, T generatedEvent)
        where T : IDomainEvent => generatedEvent;
}