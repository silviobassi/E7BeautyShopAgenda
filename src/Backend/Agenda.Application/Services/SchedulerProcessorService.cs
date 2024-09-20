using Agenda.Communication.Commands.Scheduler;
using Agenda.Domain.Entities;
using Agenda.Domain.Extensions;

namespace Agenda.Application.Services;

/// <summary>
/// Service responsible for processing the scheduler based on the provided command.
/// </summary>
/// <param name="command">The command containing the scheduler details.</param>
public class SchedulerProcessorService(CreateSchedulerCommand command)
{
    public Scheduler Schedule { get; } = command;
    public long ScheduleId => Schedule.Id;

    /// <summary>
    /// Processes the scheduler by adding appointments based on the command details.
    /// </summary>
    public void Process()
    {
        for (var date = command.StartAtSchedule;
             date <= command.EndAtSchedule;
             date = date.AddDays(1))
        {
            if (date.IsDayOffAt(DaysOff)) continue;
            AddAppointmentsForDate(date);
        }
    }

    /// <summary>
    /// Adds appointments to the schedule for a specific date.
    /// </summary>
    /// <param name="date">The date to add appointments for.</param>
    private void AddAppointmentsForDate(DateOnly date)
    {
        var (startAt, endAt) = GetStartAndEndTimes(date);
        
        for (var businessHour = startAt;
             businessHour <= endAt;
             businessHour = businessHour.Add(TimeSpan.FromMinutes(command.Duration)))
        {
            var timeOnly = new TimeOnly(businessHour.Hours, businessHour.Minutes, businessHour.Seconds);
            var utcDateTimeOffset = date.ToDateTime(timeOnly).ToUniversalTime();

            var appointment = new Appointment(utcDateTimeOffset, command.Duration)
            {
                SchedulerId = Schedule.Id,
            };
            Schedule.AddAppointment(appointment);
        }
    }

    private (TimeSpan startAt, TimeSpan endAt) GetStartAndEndTimes(DateOnly date)
    {
        return date.IsWeekday()
            ? (command.StartAtWeekday, command.EndAtWeekday)
            : (command.StartAtWeekend, command.EndAtWeekend);
    }

    public IList<DayOff> DaysOff => Schedule.DaysOff;

    public IList<Appointment> Appointments => Schedule.Appointments;
}