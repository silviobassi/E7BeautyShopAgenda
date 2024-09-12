using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class BusinessHourTest
{
    [Fact]
    public void Should_CreatingBusinessHourInstance()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        businessHour.CreatedAt.Should().NotBeNull();
        businessHour.StartAt.Should().Be(startAt);
        businessHour.EndAt.Should().Be(endAt);
        businessHour.Active.Should().BeTrue();
        businessHour.Available.Should().BeTrue();
        businessHour.Duration.Should().Be(duration);
    }

    [Fact]
    public void Should_UpdateBusinessHourInstance()
    {
        var (id, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration,professionalId);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = endAt.AddDays(1);
        const int newDuration = 45;

        businessHour.Update(id, newStartAt, newEndAt, newDuration,professionalId);

        businessHour.CreatedAt.Should().NotBeNull();
        businessHour.UpdatedAt.Should().NotBeNull();
        businessHour.StartAt.Should().Be(newStartAt);
        businessHour.EndAt.Should().Be(newEndAt);
        businessHour.Active.Should().BeTrue();
        businessHour.Available.Should().BeTrue();
        businessHour.Duration.Should().Be(newDuration);
    }
    
    [Fact]
    public void Should_MakeUnavailableBusinessHour()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        businessHour.ToggleAvailability();
        
        businessHour.Available.Should().BeFalse();
    }
    
    [Fact]
    public void Should_MakeAvailableBusinessHour()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        businessHour.ToggleAvailability();
        businessHour.ToggleAvailability();
        
        businessHour.Available.Should().BeTrue();
    }
}