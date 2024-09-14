using Agenda.Domain.Errors;
using Agenda.Domain.Events;
using static Agenda.Domain.Errors.ErrorMessages;

namespace Agenda.Domain.Entities;

public class Appointment : Entity
{
    public DateTimeOffset AppointmentHours { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ClientId { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Appointment(DateTimeOffset appointmentHours, int duration)
    {
        AppointmentHours = appointmentHours;
        Active = true;
        Available = true;
        Duration = duration;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, DateTimeOffset newStartAt, int newDuration)
    {
        Id = id;
        AppointmentHours = newStartAt;
        Duration = newDuration;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Result Schedule(TimeReservedEvent timeReservedEvent, long clientId)
    {
        if (IsClientSchedule) return Result.Fail(ClientScheduledMessage, ClientScheduledCode);
        UpdateScheduleState(clientId: clientId, available: false);
        RegisterEvent(timeReservedEvent);
        return Result.Ok();
    }

    public Result Cancel(TimeCanceledEvent timeCanceledEvent)
    {
        if (IsNotClientSchedule) return Result.Fail(NoClientScheduledMessage, NoClientScheduledCode);

        if (IsLessThanTwoHoursBefore)
            return Result.Fail(LessThanTwoHoursBeforeMessage, LessThanTwoHoursBeforeCode);

        UpdateScheduleState();
        RegisterEvent(timeCanceledEvent);
        return Result.Ok();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private bool IsClientSchedule => ClientId > 0L;

    private bool IsNotClientSchedule => !IsClientSchedule;

    private bool IsLessThanTwoHoursBefore => AppointmentHours.Subtract(DateTimeOffset.UtcNow).TotalHours < 2;

    private void UpdateScheduleState(long clientId = 0L, bool available = true)
    {
        ClientId = clientId;
        Available = available;
    }
    
    private void RegisterEvent(IDomainEvent domainEvent)
    {
        var timeReserved = this.EmitEvent(domainEvent);
        _domainEvents.Add(timeReserved);
    }
}