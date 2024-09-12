using Bogus;

namespace Test.CommonUtilities.Entities;

public abstract class BusinessHourBuilder
{
    public static (long id, DateTimeOffset startAt, DateTimeOffset endAt, int duration, long professionalId) Build()
    {
        var faker = new Faker();

        const long id = 1;
        var startAt = faker.Date.Recent();
        var endAt = faker.Date.Future();
        var duration = faker.Random.Int(30, 60);
        const long professionalId = 3;

        return (id, startAt, endAt, duration, professionalId);
    }
}