using Agenda.Domain.ValueObjects;

namespace Test.CommonUtilities.ValueObjects;

public abstract class WeekendBuilder
{
    public static (TimeSpan startAt, TimeSpan endAt, Weekend weekend) Build()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(12, 0, 0);
        var weekend = new Weekend(startAt, endAt);
        return (startAt, endAt, weekend);
    }
}