namespace Agenda.Communication.Commands.Appointment;

public sealed record CreateAppointmentCommand(
    DateTimeOffset AppointmentHour,
    int Duration
)
{
    public static implicit operator Domain.Entities.Appointment(CreateAppointmentCommand? command)
    {
        return command is null
            ? command
            : new Domain.Entities.Appointment(command.AppointmentHour, command.Duration);
    }
}