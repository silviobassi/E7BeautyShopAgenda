using System.Runtime.InteropServices.JavaScript;
using Agenda.Domain.ValueObjects;

namespace Agenda.Domain.Entities;

public sealed class Catalog : Entity
{
    public string DescriptionName { get; private set; } = string.Empty;

    public decimal DescriptionPrice { get; private set; }
    
    public Catalog()
    {
    }

    public Catalog(CatalogDescription? description)
    {
        DescriptionName = description!.Name;
        DescriptionPrice = description.Price;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, CatalogDescription newDescription)
    {
        Id = id;
        DescriptionName = newDescription!.Name;
        DescriptionPrice = newDescription.Price;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}