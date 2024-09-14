using Agenda.Domain.ValueObjects;
using FluentAssertions;
using Test.CommonUtilities.ValueObjects;

namespace Tests.Domain.ObjectValues;

public class WeekendTest
{
    [Fact]
    public void Should_CreateWeekendInstance()
    {
        var (startAt, endAt, _) = WeekendBuilder.Build();

        var weekend = new Weekend(startAt, endAt);

        weekend.StartAt.Should().Be(startAt);
        weekend.EndAt.Should().Be(endAt);
    }
}