using Agenda.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain.ObjectValues;

public class WeekdayTest
{
    [Fact]
    public void Should_CreateWeekdayInstance()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(12, 0, 0);

        var weekend = new Weekday(startAt, endAt);

        weekend.StartAt.Should().Be(startAt);
        weekend.EndAt.Should().Be(endAt);
    }
}