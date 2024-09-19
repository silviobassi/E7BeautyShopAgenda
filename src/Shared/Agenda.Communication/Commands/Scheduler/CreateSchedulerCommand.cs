using Agenda.Communication.Commands.DayOff;

namespace Agenda.Communication.Commands.Scheduler;

public sealed record CreateSchedulerCommand
{
    public DateTimeOffset StartAtSchedule { get; init; }
    public DateTimeOffset EndAtSchedule { get; init; }
    public TimeSpan StartAtWeekend { get; init; }
    public TimeSpan EndAtWeekend { get; init; }
    public TimeSpan StartAtWeekday { get; init; }
    public TimeSpan EndAtWeekday { get; init; }
    public int Duration { get; init; }
    public IEnumerable<CreateDayOffCommand> DaysOff { get; init; } = [];
    public long ProfessionalId { get; init; }

    public static implicit operator Domain.Entities.Scheduler(CreateSchedulerCommand? command)
    {
        return command is null
            ? command
            : new Domain.Entities.Scheduler(command.ProfessionalId);
    }
}