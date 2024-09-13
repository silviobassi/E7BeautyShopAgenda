using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class BusinessHourTest
{
    [Fact]
    public void Should_CreatingBusinessHourInstance()
    {
        var (_, startAt, endAt, duration) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration);

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
        var (id, startAt, endAt, duration) = BusinessHourBuilder.Build();

        BusinessHour businessHour = new(startAt, endAt, duration);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = endAt.AddDays(1);
        const int newDuration = 45;

        businessHour.Update(id, newStartAt, newEndAt, newDuration);

        businessHour.CreatedAt.Should().NotBeNull();
        businessHour.UpdatedAt.Should().NotBeNull();
        businessHour.StartAt.Should().Be(newStartAt);
        businessHour.EndAt.Should().Be(newEndAt);
        businessHour.Active.Should().BeTrue();
        businessHour.Available.Should().BeTrue();
        businessHour.Duration.Should().Be(newDuration);
    }

    [Fact]
    public void Should_Success_CancelBusinessHour()
    {
        var (_, _, endAt, duration) = BusinessHourBuilder.Build();
        const long clientId = 1L;
        
        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(121);
        
        BusinessHour businessHour = new(twoHoursAfter, endAt, duration);

        businessHour.Schedule(clientId);

        businessHour.ClientId.Should().Be(clientId);

        businessHour.Cancel();
        
        businessHour.Available.Should().BeTrue();
        businessHour.ClientId.Should().Be(0);
        businessHour.DomainEvents.Should().HaveCount(2);
    }
    
    
    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNo_Scheduling()
    {
        var (_, _, endAt, duration) = BusinessHourBuilder.Build();
        const long clientId = 1L;
        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(120);
        
        BusinessHour businessHour = new(twoHoursAfter, endAt, duration);

        businessHour.Schedule(clientId);

        businessHour.ClientId.Should().Be(clientId);

        var result = businessHour.Cancel();
        
        businessHour.Available.Should().BeFalse();
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().Be("O horário não pode ser cancelado com menos de 2 horas de antecedência");
        result.ErrorCode.Should().Be(3);
    }
    
    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNoTimeForTolerance()
    {
        var (_, startAt, endAt, duration) = BusinessHourBuilder.Build();
        
        BusinessHour businessHour = new(startAt, endAt, duration);

        var result = businessHour.Cancel();

        businessHour.Available.Should().BeTrue();
        businessHour.ClientId.Should().Be(0);
        result.Message.Should().Be("Não há um cliente agendado para este horário");
        result.ErrorCode.Should().Be(2);
    }

    [Fact]
    public void Should_Success_ScheduleBusinessHour()
    {
        var (_, startAt, endAt, duration) = BusinessHourBuilder.Build();
        const long clientId = 1L;

        BusinessHour businessHour = new(startAt, endAt, duration);

        var result = businessHour.Schedule(clientId);
        
        businessHour.Available.Should().BeFalse();
        businessHour.DomainEvents.Should().ContainSingle();
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().BeEmpty();
    }

    [Fact]
    public void Should_Fail_ScheduleBusinessHour()
    {
        var (_, startAt, endAt, duration) = BusinessHourBuilder.Build();
        const long clientId = 1L;

        BusinessHour businessHour = new(startAt, endAt, duration);

        businessHour.Schedule(clientId);
        businessHour.Schedule(2);

        var result = businessHour.Schedule(clientId);

        businessHour.Available.Should().BeFalse();
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().Be("Há um cliente agendado para este horário");
        result.ErrorCode.Should().Be(1);
    }
    
    [Fact]
    public void Should_Clear_Events()
    {
        var (_, startAt, endAt, duration) = BusinessHourBuilder.Build();
        const long clientId = 1L;

        BusinessHour businessHour = new(startAt, endAt, duration);

        var result = businessHour.Schedule(clientId);
        businessHour.ClearDomainEvents();
        
        businessHour.Available.Should().BeFalse();
        businessHour.DomainEvents.Should().BeEmpty();
        
        businessHour.ClientId.Should().Be(clientId);
        result.Message.Should().BeEmpty();
    }
}