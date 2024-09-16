using Agenda.Communication.Commands.Appointment;
using Agenda.Communication.Commands.DayOff;

namespace Agenda.Communication.Commands.Scheduler;

public sealed record CreateSchedulerCommand(
    DateTimeOffset StartAtSchedule,
    DateTimeOffset EndAtSchedule,
    TimeSpan StartAtWeekend,
    TimeSpan EndAtWeekend,
    TimeSpan StartAtWeekday,
    TimeSpan EndAtWeekday,
    int Duration,
    IEnumerable<CreateDayOffCommand> DaysOff,
    long ProfessionalId
)
{
    public static implicit operator Domain.Entities.Scheduler(CreateSchedulerCommand? command)
    {
        return command is null
            ? command
            : new Domain.Entities.Scheduler(command.ProfessionalId);
    }
}