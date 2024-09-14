using Agenda.Domain.ValueObjects;

namespace Test.CommonUtilities.ValueObjects;

public abstract class WeekdayBuilder
{
    public static (TimeSpan startAt, TimeSpan endAt, Weekday weekday) Build()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(12, 0, 0);
        var weekday = new Weekday(startAt, endAt);
        return (startAt, endAt, weekday);
    }
}