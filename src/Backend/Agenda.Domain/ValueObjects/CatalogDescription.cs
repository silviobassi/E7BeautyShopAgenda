namespace Agenda.Domain.ValueObjects;

public sealed class CatalogDescription(string name, decimal price)
{
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
}