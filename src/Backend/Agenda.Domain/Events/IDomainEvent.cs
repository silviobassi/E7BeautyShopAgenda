﻿namespace Agenda.Domain.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}