using Agenda.Communication.Commands.Scheduler;
using Agenda.Domain.Entities;
using Agenda.Domain.Extensions;

namespace Agenda.Application.Services;

public class SchedulerProcessorService(CreateSchedulerCommand command)
{
    public Scheduler Schedule { get; } = command;
    public long ScheduleId => Schedule.Id;
    public IList<DayOff> DaysOff => Schedule.DaysOff;
    public IList<Appointment> Appointments => Schedule.Appointments;

    public void Process()
    {
        for (var date = command.StartAtSchedule.Date;
             date <= command.EndAtSchedule.Date;
             date = date.AddDays(1))
        {
            if (date.IsDayOffAt(Schedule.DaysOff)) continue;
            AddAppointmentToSchedule(date);
        }
    }

    private void AddAppointmentToSchedule(DateTime date)
    {
        var startAt = date.IsWeekday() ? command.StartAtWeekday : command.StartAtWeekend;
        var endAt = date.IsWeekday() ? command.EndAtWeekday : command.EndAtWeekend;

        for (var hour = startAt; hour <= endAt; hour = hour.Add(TimeSpan.FromMinutes(command.Duration)))
        {
            var appointment = new Appointment(date.Add(hour), command.Duration)
            {
                SchedulerId = Schedule.Id,
            };
            Schedule.AddAppointment(appointment);
        }
    }
}