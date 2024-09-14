using Agenda.Domain.ValueObjects;
using FluentAssertions;
using Test.CommonUtilities.ValueObjects;

namespace Tests.Domain.ObjectValues;

public class WeekdayTest
{
    [Fact]
    public void Should_CreateWeekdayInstance()
    {
        var (startAt, endAt, _) = WeekdayBuilder.Build();

        var weekend = new Weekday(startAt, endAt);

        weekend.StartAt.Should().Be(startAt);
        weekend.EndAt.Should().Be(endAt);
    }
}