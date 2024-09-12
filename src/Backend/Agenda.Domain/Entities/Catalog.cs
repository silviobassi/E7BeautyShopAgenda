using Agenda.Domain.ValueObjects;

namespace Agenda.Domain.Entities;

public sealed class Catalog(CatalogDescription description) : Entity
{
    private CatalogDescription? Description { get; set; } = description;

    public void Update(long id, CatalogDescription description)
    {
        Id = id;
        Description = description;
    }

    public string? GetName => Description?.Name;

    public decimal? GetPrice => Description?.Price;
}