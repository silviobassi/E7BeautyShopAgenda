using Agenda.Domain.Errors;
using Agenda.Domain.Events;

namespace Agenda.Domain.Entities;

public class Scheduling : Entity
{
    public DateTimeOffset AppointmentHours { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ClientId { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Scheduling(DateTimeOffset appointmentHours, int duration)
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

    public Result Schedule(long clientId)
    {
        if (IsClient) return Result.Fail("Há um cliente agendado para este horário", 1);
        ClientId = clientId;
        Available = false;
        var timeReserved = this.EmitReservedTimeEvent(new TimeReservedEvent(AppointmentHours, Duration, ClientId));
        _domainEvents.Add(timeReserved);
        return Result.Ok();
    }

    public Result Cancel()
    {
        if (IsNotClient) return Result.Fail("Não há um cliente agendado para este horário", 2);

        if (IsLessThanTwoHoursBefore)
            return Result.Fail("O horário não pode ser cancelado com menos de 2 horas de antecedência", 3);

        const string reason = "Cliente cancelou o horário por motivos pessoais";
        Available = true;
        ClientId = 0L;
        var timeCanceled = this.EmitReservedTimeEvent(new TimeCanceledEvent(AppointmentHours, Duration, ClientId, reason));
        _domainEvents.Add(timeCanceled);
        return Result.Ok();
    }


    public void ClearDomainEvents() => _domainEvents.Clear();

    private bool IsClient => ClientId > 0L;

    private bool IsNotClient => !IsClient;

    private bool IsLessThanTwoHoursBefore => AppointmentHours.Subtract(DateTimeOffset.UtcNow).TotalHours < 2;
}