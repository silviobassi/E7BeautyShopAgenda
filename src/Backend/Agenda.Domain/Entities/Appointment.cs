using Agenda.Domain.Errors;
using Agenda.Domain.Events;
using static Agenda.Domain.Errors.Result;

namespace Agenda.Domain.Entities;

public class Appointment : Entity
{
    public DateTimeOffset AppointmentHour { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ClientId { get; private set; }

    public long ProfessionalId { get; private set; }

    private IEnumerable<DayOff> DayOffs { get; } = [];

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Appointment(DateTimeOffset appointmentHour, int duration, long professionalId)
    {
        AppointmentHour = appointmentHour;
        Duration = duration;
        ProfessionalId = professionalId;
        Available = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, DateTimeOffset newStartAt, int newDuration, long professionalId)
    {
        Id = id;
        AppointmentHour = newStartAt;
        Duration = newDuration;
        ProfessionalId = professionalId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Result Schedule(TimeReservedEvent timeReservedEvent, long clientId)
    {
        if (IsClientSchedule) return new AlreadyClientSchedule();
        UpdateScheduleState(clientId: clientId, available: false);
        RegisterEvent(timeReservedEvent);
        return Success();
    }

    public Result Cancel(TimeCanceledEvent timeCanceledEvent)
    {
        if (IsNotClientSchedule) return new NoClientSchedule();
        if (IsLessThanTwoHoursBefore) return new AppointmentLessThanTwoHours();

        UpdateScheduleState();
        RegisterEvent(timeCanceledEvent);
        return Success();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private bool IsClientSchedule => ClientId > 0L;

    private bool IsNotClientSchedule => !IsClientSchedule;

    private bool IsLessThanTwoHoursBefore => AppointmentHour.Subtract(DateTimeOffset.UtcNow).TotalHours < 2;

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