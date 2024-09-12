using Bogus;

namespace Test.CommonUtilities.Entities;

public abstract class DayRestBuilder
{
    public static (long id, DayOfWeek dayOfWeek, long agendaId) Build()
    {
        var faker = new Faker();
        var id = faker.Random.Long(0, 100);
        var dayOnWeek = faker.Random.Enum<DayOfWeek>();
        const long agendaId = 2;

        return (id, dayOnWeek, agendaId);
    }
}