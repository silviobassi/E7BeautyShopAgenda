using Agenda.Domain.ValueObjects;

namespace Agenda.Domain.Entities;

public sealed class Catalog : Entity
{
    private CatalogDescription? Description { get; set; }

    public Catalog(CatalogDescription description)
    {
        Description = description;
        CreatedAt = DateTimeOffset.Now;
    }

    public void Update(long id, CatalogDescription newDescription)
    {
        Id = id;
        Description = newDescription;
        UpdatedAt = DateTimeOffset.Now;
    }

    public string? GetName => Description?.Name;

    public decimal? GetPrice => Description?.Price;
}