using Agenda.Domain.Errors;
using Agenda.Domain.Events;

namespace Agenda.Domain.Entities;

public class BusinessHour : Entity
{
    public DateTimeOffset StartAt { get; private set; }
    public DateTimeOffset EndAt { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ClientId { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public BusinessHour(DateTimeOffset startAt, DateTimeOffset endAt, int duration)
    {
        StartAt = startAt;
        EndAt = endAt;
        Active = true;
        Available = true;
        Duration = duration;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, DateTimeOffset newStartAt, DateTimeOffset newEndAt,
        int newDuration)
    {
        Id = id;
        StartAt = newStartAt;
        EndAt = newEndAt;
        Duration = newDuration;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Result Schedule(long clientId)
    {
        if (IsClient) return Result.Fail("Há um cliente agendado para este horário", 1);
        ClientId = clientId;
        Available = false;
        var timeReserved = this.EmitReservedTimeEvent(new TimeReservedEvent(StartAt, Duration, ClientId));
        _domainEvents.Add(timeReserved);
        return Result.Ok();
    }

    public Result Cancel()
    {
        if (IsNotClient) return Result.Fail("Não há um cliente agendado para este horário", 2);

        if (IsLessThanTwoHoursBefore())
            return Result.Fail("O horário não pode ser cancelado com menos de 2 horas de antecedência", 3);
        
        const string reason = "Cliente cancelou o horário por motivos pessoais";
        Available = true;
        ClientId = 0L;
        var timeCanceled = this.EmitReservedTimeEvent(new TimeCanceledEvent(StartAt, Duration, ClientId, reason));
        _domainEvents.Add(timeCanceled);
        return Result.Ok();
    }


    public void ClearDomainEvents() => _domainEvents.Clear();

    private bool IsClient => ClientId > 0L;

    private bool IsNotClient => !IsClient;

    private bool IsLessThanTwoHoursBefore()
    {
        var hourSchedule = StartAt;
        var currentHour = DateTimeOffset.UtcNow;

        var totalHours = hourSchedule.Subtract(currentHour).TotalHours;
        var twoHoursBefore = totalHours < 2;
        return twoHoursBefore;
    }
}