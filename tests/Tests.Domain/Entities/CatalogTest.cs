using Agenda.Domain.Entities;
using Agenda.Domain.ValueObjects;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class CatalogTest
{
    [Fact]
    public void Should_CreatingCatalogInstance()
    {
        var (_, description, _) = CatalogBuilder.Build();

        Catalog catalog = new(description);

        catalog.Should().NotBeNull();
        catalog.CreatedAt.Should().BeAfter(DateTimeOffset.MinValue);
        catalog.DescriptionName.Should().Be(description.Name);
        catalog.DescriptionPrice.Should().Be(description.Price);
    }

    [Fact]
    public void Should_UpdatingCatalogInstance()
    {
        var (id, description, _) = CatalogBuilder.Build();

        Catalog catalog = new(description);

        const string corteDeCabeloSobrancelhaBarba = "Corte de Cabelo + sobrancelha + Barba";
        const int expectedPrice = 100;

        catalog.Update(id, new CatalogDescription(corteDeCabeloSobrancelhaBarba, expectedPrice));

        catalog.Should().NotBeNull();

        catalog.CreatedAt.Should().NotBeNull();
        catalog.UpdatedAt.Should().NotBeNull();
        catalog.DescriptionName.Should().NotBe(description.Name);
        catalog.DescriptionPrice.Should().NotBe(description.Price);
        catalog.DescriptionName.Should().Be(corteDeCabeloSobrancelhaBarba);
        catalog.DescriptionPrice.Should().Be(expectedPrice);
    }
}