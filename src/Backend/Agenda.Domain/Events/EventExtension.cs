using Agenda.Domain.Entities;

namespace Agenda.Domain.Events;

public static class EventExtension
{
    public static T EmitReservedTimeEvent<T>(this Scheduling scheduling, T generatedEvent)
        where T : IDomainEvent => generatedEvent;
}