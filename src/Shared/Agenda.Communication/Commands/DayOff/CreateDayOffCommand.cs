namespace Agenda.Communication.Commands.DayOff;

public sealed record CreateDayOffCommand(DayOfWeek DayOnWeek)
{
    public static implicit operator Domain.Entities.DayOff(CreateDayOffCommand? command)
        => command is null ? command : new Domain.Entities.DayOff(command.DayOnWeek);
}