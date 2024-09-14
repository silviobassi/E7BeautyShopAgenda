using Agenda.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain.ObjectValues;

public class WeekendTest
{
    [Fact]
    public void Should_CreateWeekendInstance()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(12, 0, 0);

        var weekend = new Weekend(startAt, endAt);

        weekend.StartAt.Should().Be(startAt);
        weekend.EndAt.Should().Be(endAt);
    }
}