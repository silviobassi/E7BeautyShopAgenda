using Agenda.Domain.Entities;
using Agenda.Domain.Errors;
using Agenda.Domain.Events;
using FluentAssertions;
using Test.CommonUtilities.Entities;
using static Agenda.Domain.Errors.ResourceMessageError;

namespace Tests.Domain.Entities;

public class AppointmentTest
{
    [Fact]
    public void Should_CreatingBusinessHourInstance()
    {
        var (_, startAt, duration) = AppointmentBuilder.Build();

        Appointment appointment = new(startAt, duration);

        appointment.CreatedAt.Should().NotBeNull();
        appointment.AppointmentHours.Should().Be(startAt);
        appointment.Active.Should().BeTrue();
        appointment.Available.Should().BeTrue();
        appointment.Duration.Should().Be(duration);
    }

    [Fact]
    public void Should_UpdateBusinessHourInstance()
    {
        var (id, startAt, duration) = AppointmentBuilder.Build();

        Appointment appointment = new(startAt, duration);

        var newStartAt = startAt.AddDays(1);
        const int newDuration = 45;

        appointment.Update(id, newStartAt, newDuration);

        appointment.CreatedAt.Should().NotBeNull();
        appointment.UpdatedAt.Should().NotBeNull();
        appointment.AppointmentHours.Should().Be(newStartAt);
        appointment.Active.Should().BeTrue();
        appointment.Available.Should().BeTrue();
        appointment.Duration.Should().Be(newDuration);
    }

    [Fact]
    public void Should_Success_CancelBusinessHour()
    {
        var (_, _, duration) = AppointmentBuilder.Build();
        const long clientId = 1L;

        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(121);

        Appointment appointment = new(twoHoursAfter, duration);

        TimeReservedEvent timeReservedEvent = new (appointment.AppointmentHours, appointment.Duration, clientId);
        appointment.Schedule(timeReservedEvent, clientId);

        appointment.ClientId.Should().Be(clientId);

        TimeCanceledEvent timeCanceledEvent =
            new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId, "Vou ao médico");

        appointment.Cancel(timeCanceledEvent);

        appointment.Available.Should().BeTrue();
        appointment.ClientId.Should().Be(0);
        appointment.DomainEvents.Should().HaveCount(2);
    }


    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNo_Scheduling()
    {
        var (_, _, duration) = AppointmentBuilder.Build();
        const long clientId = 1L;
        var twoHoursAfter = DateTimeOffset.UtcNow.AddMinutes(120);

        Appointment appointment = new(twoHoursAfter, duration);

        TimeReservedEvent timeReservedEvent = new (appointment.AppointmentHours, appointment.Duration, clientId);
        appointment.Schedule(timeReservedEvent, clientId);

        appointment.ClientId.Should().Be(clientId);

        TimeCanceledEvent timeCanceledEvent =
            new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId, "Vou ao médico");

        var result = appointment.Cancel(timeCanceledEvent);
        
        appointment.Available.Should().BeFalse();
        appointment.ClientId.Should().Be(clientId);
        result.Error?.Detail.Should().Be(LESS_THAN_TWO_HOURS);
        result.Error?.ErrorType.Should().Be(ErrorType.BusinessRule);
        result.Error?.ErrorTypeName.Should().Be(nameof(AppointmentLessThanTwoHours));
    }

    [Fact]
    public void Should_Fail_CancelBusinessHour_When_ThereIsNoTimeForTolerance()
    {
        var (_, startAt, duration) = AppointmentBuilder.Build();

        Appointment appointment = new(startAt, duration);

        TimeCanceledEvent timeCanceledEvent =
            new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId, "Vou ao médico");
        var result = appointment.Cancel(timeCanceledEvent);

        appointment.Available.Should().BeTrue();
        appointment.ClientId.Should().Be(0);
        result.Error?.Detail.Should().Be(NO_CLIENT_SCHEDULE);
        result.Error?.ErrorType.Should().Be(ErrorType.BusinessRule);
        result.Error?.ErrorTypeName.Should().Be(nameof(NoClientSchedule));
    }

    [Fact]
    public void Should_Success_ScheduleBusinessHour()
    {
        var (_, startAt, duration) = AppointmentBuilder.Build();
        const long clientId = 1L;

        Appointment appointment = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId);
        var result = appointment.Schedule(timeReservedEvent,clientId);

        appointment.Available.Should().BeFalse();
        appointment.DomainEvents.Should().ContainSingle();
        appointment.ClientId.Should().Be(clientId);
        result.Error.Should().BeNull();
       
    }

    [Fact]
    public void Should_Fail_ScheduleBusinessHour()
    {
        var (_, startAt, duration) = AppointmentBuilder.Build();
        const long clientId = 1L;

        Appointment appointment = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId);
        appointment.Schedule(timeReservedEvent, clientId);
        var result = appointment.Schedule(timeReservedEvent, clientId);

        appointment.Available.Should().BeFalse();
        appointment.ClientId.Should().Be(clientId);
        result.Error?.Detail.Should().Be(ALREADY_CLIENT_SCHEDULE);
        result.Error?.ErrorType.Should().Be(ErrorType.BusinessRule);
        result.Error?.ErrorTypeName.Should().Be(nameof(AlreadyClientSchedule));
    }

    [Fact]
    public void Should_Clear_Events()
    {
        var (_, startAt, duration) = AppointmentBuilder.Build();
        const long clientId = 1L;

        Appointment appointment = new(startAt, duration);

        TimeReservedEvent timeReservedEvent = new(appointment.AppointmentHours, appointment.Duration, appointment.ClientId);
        appointment.Schedule(timeReservedEvent, clientId);
        
        appointment.ClearDomainEvents();

        appointment.Available.Should().BeFalse();
        appointment.DomainEvents.Should().BeEmpty();

        appointment.ClientId.Should().Be(clientId);
    }
}