using Agenda.Domain.Entities;

namespace Agenda.Domain.Events;

public static class EventExtension
{
    public static T EmitEvent<T>(this Appointment appointment, T generatedEvent)
        where T : IDomainEvent => generatedEvent;
}