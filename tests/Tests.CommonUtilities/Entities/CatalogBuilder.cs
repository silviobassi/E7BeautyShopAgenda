using Agenda.Domain.Entities;
using Agenda.Domain.ValueObjects;
using Bogus;
using Test.CommonUtilities.ValueObjects;

namespace Test.CommonUtilities.Entities;

public abstract class CatalogBuilder
{
    public static  (long id, CatalogDescription description) Build()
    {
        var faker = new Faker();
        var id = faker.Random.Long(0, 1000);
        var (name, price) = CatalogDescriptionBuilder.Build();

        var description = new CatalogDescription(name, price);
        return (id, description);
    }
}