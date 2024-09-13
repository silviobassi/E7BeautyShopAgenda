using Agenda.Domain.Entities;
using Agenda.Domain.Events;
using FluentAssertions;
using Test.CommonUtilities.Entities;
using static Agenda.Domain.Errors.ErrorMessages;

namespace Tests.Domain.Entities;

public class SchedulingTest
{
    [Fact]
    public void Should_CreatingBusinessHourInstance()
    {
        var (_, startAt, duration) = SchedulingBuilder.Build();

        Scheduling scheduling = new(startAt, duration);

        scheduling.CreatedAt.Should().NotBeNull();
        scheduling.AppointmentHours.Should().Be(startAt);
        scheduling.Active.Should().BeTrue();
        scheduling.Available.Should().BeTrue();
        scheduling.Duration.Should().Be(duration);
    }

    [Fact]
    public void Should_UpdateBusinessHourInstance()
    {
        var (id, startAt, duration) = SchedulingBuilder.Build();

        Scheduling scheduling = new(startAt, duration);

        var newStartAt = startAt.AddDays(1);
        const int newDuration = 45;

        scheduling.Update(id, newStartAt, newDuration);

        scheduling.CreatedAt.Should().NotBeNull();
        scheduling.UpdatedAt.Should().NotBeNull();
        scheduling.AppointmentHours.Should().Be(newStartAt);
        scheduling.Active.Should().BeTrue();
        scheduling.Available.Should().BeTrue();
        scheduling.Duration.Should().Be(newDuration);
    }

    [Fact]
    public void Should_Success_CancelBusinessHour()
    {
        var (_, _, duration) = SchedulingBuilder.Build();
        const long clientId = 1L;

        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(121);

        Scheduling scheduling = new(twoHoursAfter, duration);

        TimeReservedEvent timeReservedEvent = new (scheduling.AppointmentHours, scheduling.Duration, clientId);
        scheduling.Schedule(timeReservedEvent, clientId);

        scheduling.ClientId.Should().Be(clientId);

        TimeCanceledEvent timeCanceledEvent =
            new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId, "Vou ao médico");

        scheduling.Cancel(timeCanceledEvent);

        scheduling.Available.Should().BeTrue();
        scheduling.ClientId.Should().Be(0);
        scheduling.DomainEvents.Should().HaveCount(2);
    }


    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNo_Scheduling()
    {
        var (_, _, duration) = SchedulingBuilder.Build();
        const long clientId = 1L;
        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(120);

        Scheduling scheduling = new(twoHoursAfter, duration);

        TimeReservedEvent timeReservedEvent = new (scheduling.AppointmentHours, scheduling.Duration, clientId);
        scheduling.Schedule(timeReservedEvent, clientId);

        scheduling.ClientId.Should().Be(clientId);

        TimeCanceledEvent timeCanceledEvent =
            new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId, "Vou ao médico");

        var result = scheduling.Cancel(timeCanceledEvent);
        
        scheduling.Available.Should().BeFalse();
        scheduling.ClientId.Should().Be(clientId);
        result.Message.Should().Be(LessThanTwoHoursBeforeMessage);
        result.ErrorCode.Should().Be(LessThanTwoHoursBeforeCode);
    }

    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNoTimeForTolerance()
    {
        var (_, startAt, duration) = SchedulingBuilder.Build();

        Scheduling scheduling = new(startAt, duration);

        TimeCanceledEvent timeCanceledEvent =
            new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId, "Vou ao médico");
        var result = scheduling.Cancel(timeCanceledEvent);

        scheduling.Available.Should().BeTrue();
        scheduling.ClientId.Should().Be(0);
        result.Message.Should().Be(NoClientScheduledMessage);
        result.ErrorCode.Should().Be(NoClientScheduledCode);
    }

    [Fact]
    public void Should_Success_ScheduleBusinessHour()
    {
        var (_, startAt, duration) = SchedulingBuilder.Build();
        const long clientId = 1L;

        Scheduling scheduling = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId);
        var result = scheduling.Schedule(timeReservedEvent,clientId);

        scheduling.Available.Should().BeFalse();
        scheduling.DomainEvents.Should().ContainSingle();
        scheduling.ClientId.Should().Be(clientId);
        result.Message.Should().BeEmpty();
    }

    [Fact]
    public void Should_Fail_ScheduleBusinessHour()
    {
        var (_, startAt, duration) = SchedulingBuilder.Build();
        const long clientId = 1L;

        Scheduling scheduling = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId);
        scheduling.Schedule(timeReservedEvent, clientId);
        var result = scheduling.Schedule(timeReservedEvent, clientId);

        scheduling.Available.Should().BeFalse();
        scheduling.ClientId.Should().Be(clientId);
        result.Message.Should().Be(ClientScheduledMessage);
        result.ErrorCode.Should().Be(ClientScheduledCode);
    }

    [Fact]
    public void Should_Clear_Events()
    {
        var (_, startAt, duration) = SchedulingBuilder.Build();
        const long clientId = 1L;

        Scheduling scheduling = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(scheduling.AppointmentHours, scheduling.Duration, scheduling.ClientId);
        var result = scheduling.Schedule(timeReservedEvent, clientId);
        
        scheduling.ClearDomainEvents();

        scheduling.Available.Should().BeFalse();
        scheduling.DomainEvents.Should().BeEmpty();

        scheduling.ClientId.Should().Be(clientId);
        result.Message.Should().BeEmpty();
    }
}