using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;
namespace Tests.Domain.Entities;

public class SchedulerTest
{
    [Fact]
    public void Should_CreatingSchedulerInstance()
    {
        const long professionalId = 1;

        var scheduler = new Scheduler(professionalId);

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.DaysOff.Should().BeEmpty();
        scheduler.ProfessionalId.Should().Be(professionalId);
    }

    [Fact]
    public void Should_UpdatingSchedulerInstance()
    {
        const long professionalId = 1;

        var scheduler = new Scheduler(professionalId);
        
        const long newProfessionalId = 2;

        scheduler.Update(2L, newProfessionalId);

        scheduler.Id.Should().Be(2L);
        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().NotBeNull();
        scheduler.DaysOff.Should().BeEmpty();
        scheduler.ProfessionalId.Should().Be(newProfessionalId);
    }

    [Fact]
    public void Should_AddDayOffInScheduler()
    {
        const long professionalId = 1;

        var scheduler = new Scheduler(professionalId);

        scheduler.AddDayOff(new DayOff(DayOfWeek.Saturday));
        scheduler.AddDayOff(new DayOff(DayOfWeek.Sunday));

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.DaysOff.Should().HaveCount(2);
        scheduler.ProfessionalId.Should().Be(professionalId);
    }

    [Fact]
    public void Should_AddAppointmentInScheduler()
    {
        const long professionalId = 1;

        var scheduler = new Scheduler(professionalId);

        var (_, appointmentHour, duration) = AppointmentBuilder.Build();

        scheduler.AddAppointment(new Appointment(appointmentHour, duration));
        scheduler.AddAppointment(new Appointment(appointmentHour, duration));

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.Appointments.Should().HaveCount(2);
        scheduler.ProfessionalId.Should().Be(professionalId);
    }

    [Fact]
    public void Should_Error_AddAppointmentInScheduler_OnDayOff()
    {

        const long professionalId = 1;

        var scheduler = new Scheduler(professionalId);

        var (_, _, duration) = AppointmentBuilder.Build();
        
        var saturday = new DateTimeOffset(2024, 9, 14, 8, 0, 0, TimeSpan.Zero);

        scheduler.AddDayOff(new DayOff(DayOfWeek.Saturday));

        scheduler.AddAppointment(new Appointment(saturday, duration));

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.Appointments.Should().BeEmpty();
        scheduler.ProfessionalId.Should().Be(professionalId);
    }
}