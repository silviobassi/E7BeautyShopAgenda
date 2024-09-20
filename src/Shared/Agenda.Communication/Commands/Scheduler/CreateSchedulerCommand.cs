using Agenda.Communication.Commands.DayOff;

namespace Agenda.Communication.Commands.Scheduler;

public sealed record CreateSchedulerCommand(
    DateOnly StartAtSchedule,
    DateOnly EndAtSchedule,
    TimeSpan StartAtWeekend,
    TimeSpan EndAtWeekend,
    TimeSpan StartAtWeekday,
    TimeSpan EndAtWeekday,
    int Duration,
    IList<CreateDayOffCommand> DaysOff,
    long ProfessionalId
)
{
    public static implicit operator Domain.Entities.Scheduler(CreateSchedulerCommand? command)
    {
        if (command is null) return command;

        var scheduler = new Domain.Entities.Scheduler(command.ProfessionalId);
        foreach (Domain.Entities.DayOff dayOff in command.DaysOff) scheduler.AddDayOff(dayOff);

        return scheduler;
    }
}