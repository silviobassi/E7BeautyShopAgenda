using Agenda.Domain.ValueObjects;
using FluentAssertions;
using Test.CommonUtilities.ValueObjects;

namespace Tests.Domain.ObjectValues;

public class CatalogDescriptionTest
{
    [Fact]
    public void Should_CreatingServiceDescriptionInstance()
    {
        var (name, price) = CatalogDescriptionBuilder.Build();

        var serviceDescription = new CatalogDescription(name, price);

        serviceDescription.Name.Should().Be(name);
        serviceDescription.Price.Should().Be(price);
    }
}