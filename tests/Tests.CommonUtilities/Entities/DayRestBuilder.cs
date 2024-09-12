using Bogus;

namespace Test.CommonUtilities.Entities;

public abstract class DayRestBuilder
{
    public static (long id, DayOfWeek dayOfWeek) Build()
    {
        var faker = new Faker();
        var id = faker.Random.Long(0, 100);
        const DayOfWeek dayOnWeek = DayOfWeek.Friday;

        return (id, dayOnWeek);
    }
}