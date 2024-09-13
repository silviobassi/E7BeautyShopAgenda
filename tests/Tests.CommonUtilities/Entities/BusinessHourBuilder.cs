using Bogus;

namespace Test.CommonUtilities.Entities;

public abstract class BusinessHourBuilder
{
    public static (long id, DateTimeOffset startAt, DateTimeOffset endAt, int duration) Build()
    {
        var faker = new Faker();

        const long id = 1;
        var startAt = faker.Date.RecentOffset().ToOffset(TimeSpan.Zero);
        var endAt = faker.Date.FutureOffset().ToOffset(TimeSpan.Zero);
        var duration = faker.Random.Int(30, 60);

        return (id, startAt, endAt, duration);
    }
}