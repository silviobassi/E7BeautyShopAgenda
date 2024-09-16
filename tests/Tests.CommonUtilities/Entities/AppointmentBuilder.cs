using Bogus;

namespace Test.CommonUtilities.Entities;

public abstract class AppointmentBuilder
{
    public static (long id, DateTimeOffset appointmentHour, int duration, long professionalId) Build()
    {
        var faker = new Faker();

        const long id = 1;
        var appointmentHour = faker.Date.RecentOffset().ToOffset(TimeSpan.Zero);
        var duration = faker.Random.Int(30, 60);
        var professionalId = faker.Random.Long(1, 10);

        return (id, appointmentHour, duration, professionalId);
    }
}