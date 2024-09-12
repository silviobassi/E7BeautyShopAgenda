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
        var (_, description) = CatalogBuilder.Build();
        
        Catalog catalog = new(description);

        catalog.Should().NotBeNull();
        catalog.GetName.Should().Be(description.Name);
        catalog.GetPrice.Should().Be(description.Price);
    }
    
    [Fact]
    public void Should_UpdatingCatalogInstance()
    {
        var (id, description) = CatalogBuilder.Build();
        
        Catalog catalog = new(description);

        catalog.Should().NotBeNull();
        catalog.GetName.Should().Be(description.Name);
        catalog.GetPrice.Should().Be(description.Price);

        const string corteDeCabeloSobrancelhaBarba = "Corte de Cabelo + sobrancelha + Barba";
        const int expectedPrice = 100;
        
        catalog.Update(id, new CatalogDescription(corteDeCabeloSobrancelhaBarba, expectedPrice));
        
        catalog.Should().NotBeNull();

        catalog.GetName.Should().NotBe(description.Name);
        catalog.GetPrice.Should().NotBe(description.Price);
        catalog.GetName.Should().Be(corteDeCabeloSobrancelhaBarba);
        catalog.GetPrice.Should().Be(expectedPrice);
    }
}