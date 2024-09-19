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
    public long SchedulerId { get;  set; }
    public Catalog? Catalog { get; private set; }
    public long? CatalogId { get; set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    private Appointment()
    {
    }

    public Appointment(DateTimeOffset appointmentHour, int duration)
    {
        AppointmentHour = appointmentHour;
        CreatedAt = DateTimeOffset.UtcNow;
        Duration = duration;
        Available = true;
    }

    public void Update(long id, DateTimeOffset newStartAt, int newDuration)
    {
        Id = id;
        AppointmentHour = newStartAt;
        Duration = newDuration;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Result Schedule(TimeReservedEvent timeReservedEvent, Catalog catalog, long clientId)
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

    private void UpdateScheduleState(Catalog? catalog = default, long clientId = 0L, bool available = true)
    {
        ClientId = clientId;
        Catalog = catalog;
        Available = available;
    }

    private void RegisterEvent(IDomainEvent domainEvent)
    {
        var timeReserved = this.EmitEvent(domainEvent);
        _domainEvents.Add(timeReserved);
    }
}
