namespace Agenda.Communication.Commands.Catalog;

public record CreateCatalogCommand(string DescriptionName, decimal DescriptionPrice)
{
    public static implicit operator Domain.Entities.Catalog(CreateCatalogCommand? command)
        => command is null
            ? command
            : new Domain.Entities.Catalog(
                new Domain.ValueObjects.CatalogDescription(command.DescriptionName, command.DescriptionPrice));
}