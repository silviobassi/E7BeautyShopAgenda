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

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = endAt.AddDays(1);
        const int newDuration = 45;

        businessHour.Update(id, newStartAt, newEndAt, newDuration, professionalId);

        businessHour.CreatedAt.Should().NotBeNull();
        businessHour.UpdatedAt.Should().NotBeNull();
        businessHour.StartAt.Should().Be(newStartAt);
        businessHour.EndAt.Should().Be(newEndAt);
        businessHour.Active.Should().BeTrue();
        businessHour.Available.Should().BeTrue();
        businessHour.Duration.Should().Be(newDuration);
    }

    [Fact]
    public void Should_CancelBusinessHour()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();
        const int clientId = 1;

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        businessHour.Schedule(clientId);

        businessHour.ClientId.Should().Be(clientId);

        businessHour.Cancel();

        businessHour.Available.Should().BeFalse();
        businessHour.ClientId.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Should_Success_To_ScheduleBusinessHour()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();
        const int clientId = 1;

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        var result = businessHour.Schedule(clientId);
        
        businessHour.Available.Should().Be(result.Success);
        businessHour.DomainEvents.Should().HaveCount(2);
        
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().BeEmpty();
    }

    [Fact]
    public void Should_Fail_To_ScheduleBusinessHour()
    {
        var (_, startAt, endAt, duration, professionalId) = BusinessHourBuilder.Build();
        const int clientId = 1;

        BusinessHour businessHour = new(startAt, endAt, duration, professionalId);

        businessHour.Schedule(clientId);

        var result = businessHour.Schedule(clientId);

        businessHour.Available.Should().NotBe(result.Success);
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().Be("Há um cliente agendado para este horário");
        result.ErrorCode.Should().Be(1);
    }
}