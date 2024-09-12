using Agenda.Domain.Errors;
using Agenda.Domain.Events;

namespace Agenda.Domain.Entities;

public class BusinessHour
    : Entity
{
    public DateTimeOffset StartAt { get; private set; }
    public DateTimeOffset EndAt { get; private set; }
    public bool Active { get; private set; }
    public bool Available { get; private set; }
    public int Duration { get; private set; }
    public long ProfessionalId { get; private set; }
    public long ClientId { get; private set; }
    
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public BusinessHour(DateTimeOffset startAt, DateTimeOffset endAt, int duration, long professionalId)
    {
        StartAt = startAt;
        EndAt = endAt;
        Active = true;
        Available = true;
        Duration = duration;
        ProfessionalId = professionalId;
        CreatedAt = DateTimeOffset.Now;
    }

    public void Update(long id, DateTimeOffset newStartAt, DateTimeOffset newEndAt,
        int newDuration, long professionalId)
    {
        Id = id;
        StartAt = newStartAt;
        EndAt = newEndAt;
        Duration = newDuration;
        ProfessionalId = professionalId;
        UpdatedAt = DateTimeOffset.Now;
    }

    public Result Schedule(long clientId)
    {
        // Não pode haver cliente atribuído ao horário
        if (ClientId > 0) return Result.Fail("Há um cliente agendado para este horário", 1);

        ClientId = clientId;
        
        // O cliente tem que ser atribuído ao horário
        Available = true;
        
        // envia um evento ao cliente para informar que o horário está disponível em um micro serviço esterno
        // Publica o evento de domínio
        var businessHourReservedEvent = this.CreateBusinessHourReservedEvent(StartAt, Duration, clientId);
        _domainEvents.Add(businessHourReservedEvent);

        return Result.Ok();
    }
    
    public void Cancel()
    {
        // Envia um evento ao professional para informar que o cliente cancelou o horário
        // O cliente não pode ser removido do horário
        Available = false;
    }
    
    public void ClearDomainEvents() => _domainEvents.Clear();

}