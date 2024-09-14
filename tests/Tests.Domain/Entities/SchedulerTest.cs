using Agenda.Domain.Entities;
using Agenda.Domain.ValueObjects;
using FluentAssertions;
using Test.CommonUtilities.ValueObjects;

namespace Tests.Domain.Entities;

public class SchedulerTest
{
    [Fact]
    public void Should_CreatingSchedulerInstance()
    {
        var (_, _, weekend) = WeekendBuilder.Build();
        var (_, _, weekday) = WeekdayBuilder.Build();

        const long professionalId = 1;

        var scheduler = new Scheduler(weekend, weekday, professionalId);

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.DaysOff.Should().BeEmpty();
        scheduler.Weekend.Should().BeEquivalentTo(weekend);
        scheduler.Weekday.Should().BeEquivalentTo(weekday);
        scheduler.ProfessionalId.Should().Be(professionalId);
    }

    [Fact]
    public void Should_UpdatingSchedulerInstance()
    {
        var (_, _, weekend) = WeekendBuilder.Build();
        var (_, _, weekday) = WeekdayBuilder.Build();

        const long professionalId = 1;

        var scheduler = new Scheduler(weekend, weekday, professionalId);

        var newStartAtWeekend = new TimeSpan(8, 0, 0);
        var newEndAtWeekend = new TimeSpan(14, 0, 0);
        var newStartAtWeekday = new TimeSpan(8, 0, 0);
        var newEndAtWeekday = new TimeSpan(17, 0, 0);

        const long newProfessionalId = 2;

        var newWeekend = new Weekend(newStartAtWeekend, newEndAtWeekend);
        var newWeekday = new Weekday(newStartAtWeekday, newEndAtWeekday);
        scheduler.Update(1L, newWeekend, newWeekday, newProfessionalId);

        scheduler.Id.Should().Be(1L);
        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().NotBeNull();
        scheduler.DaysOff.Should().BeEmpty();
        scheduler.Weekend.Should().BeEquivalentTo(newWeekend);
        scheduler.Weekday.Should().BeEquivalentTo(newWeekday);
        scheduler.ProfessionalId.Should().Be(newProfessionalId);
    }
    
    [Fact]
    public void Should_AddAppointmentInScheduler()
    {
        var (_, _, weekend) = WeekendBuilder.Build();
        var (_, _, weekday) = WeekdayBuilder.Build();

        const long professionalId = 1;

        var scheduler = new Scheduler(weekend, weekday, professionalId);
        
        scheduler.AddDayOff(new DayOff(DayOfWeek.Saturday));
        scheduler.AddDayOff(new DayOff(DayOfWeek.Sunday));

        scheduler.CreatedAt.Should().NotBeNull();
        scheduler.UpdatedAt.Should().BeNull();
        scheduler.DaysOff.Should().HaveCount(2);
        scheduler.Weekend.Should().BeEquivalentTo(weekend);
        scheduler.Weekday.Should().BeEquivalentTo(weekday);
        scheduler.ProfessionalId.Should().Be(professionalId);
    }
}