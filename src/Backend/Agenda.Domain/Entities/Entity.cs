namespace Agenda.Domain.Entities;

public abstract class Entity
{
    public long Id { get; protected set; }
    public DateTimeOffset? CreatedAt { get; protected set; }
    public DateTimeOffset? UpdatedAt { get; protected set; }
}