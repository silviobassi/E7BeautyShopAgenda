using Agenda.Domain.Entities;
using Agenda.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain.Entities;

public class SchedulerTest
{
    [Fact]
    public void Should_CreatingSchedulerInstance()
    {
        List<DayOff> daysOff = [new(DayOfWeek.Monday), new(DayOfWeek.Tuesday)];
        var startAtWeekend = new TimeSpan(8, 0, 0);
        var endAtWeekend = new TimeSpan(12, 0, 0);
        var startAtWeekday = new TimeSpan(8, 0, 0);
        var endAtWeekday = new TimeSpan(18, 0, 0);

        const long professionalId = 1;

        var weekend = new Weekend(startAtWeekend, endAtWeekend);
        var weekday = new Weekday(startAtWeekday, endAtWeekday);

        var scheduler = new Scheduler(daysOff, weekend, weekday, professionalId);
        
        scheduler.DaysOff.Should().BeEquivalentTo(daysOff);
        scheduler.Weekend.Should().BeEquivalentTo(weekend);
        scheduler.Weekday.Should().BeEquivalentTo(weekday);
        scheduler.ProfessionalId.Should().Be(professionalId);
    }
}