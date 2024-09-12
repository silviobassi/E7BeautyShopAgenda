using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class BusinessHourTest
{
    [Fact]
    public void Should_CreatingBusinessHourInstance()
    {
        var (_, startAt, endAt, active, available, duration) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, active, available, duration);

        businessHour.CreatedAt.Should().BeMoreThan(TimeSpan.Zero);
        businessHour.EndAt.Should().Be(endAt);
        businessHour.Active.Should().Be(active);
        businessHour.Available.Should().Be(available);
        businessHour.Duration.Should().Be(duration);
    }

    [Fact]
    public void Should_UpdateBusinessHourInstance()
    {
        var (id, startAt, endAt, active, available, duration) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, active, available, duration);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = endAt.AddDays(1);
        const bool newActive = false;
        const bool newAvailable = false;
        const int newDuration = 45;

        businessHour.Update(id, newStartAt, newEndAt, newActive, newAvailable, newDuration);

        businessHour.CreatedAt.Should().NotBeNull();
        businessHour.UpdatedAt.Should().NotBeNull();
        businessHour.StartAt.Should().Be(newStartAt);
        businessHour.EndAt.Should().Be(newEndAt);
        businessHour.Active.Should().Be(newActive);
        businessHour.Available.Should().Be(newAvailable);
        businessHour.Duration.Should().Be(newDuration);
    }
}