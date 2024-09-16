using Agenda.Communication.Commands.Scheduler;
using Agenda.Domain.Entities;
using Agenda.Domain.Extensions;
using Agenda.Domain.Repositories;

namespace Agenda.Application.Commands.Scheduler;

public class CreateSchedulerCommandHandler(
    ISchedulerRepository schedulerRepository,
    IAppointmentRepository appointmentRepository,
    IDayOffRepository dayOffRepository,
    IUnitOfWork unitOfWork)
    : ICreateSchedulerCommandHandler
{
    public async Task Handle(CreateSchedulerCommand command)
    {
        Domain.Entities.Scheduler scheduler = command;
        
        AddDaysOffToSchedule(command, scheduler);
        AddAppointmentListToSchedule(command, scheduler);

        await schedulerRepository.CreateAsync(scheduler);

        unitOfWork.AutoDetectChangesEnabled(false);

        await dayOffRepository.AddRangeAsync(scheduler.DaysOff);
        await appointmentRepository.AddRangeAsync(scheduler.Appointments);

        unitOfWork.AutoDetectChangesEnabled(true);

        await unitOfWork.CommitAsync();
    }

    private static void AddAppointmentListToSchedule(CreateSchedulerCommand command, Domain.Entities.Scheduler scheduler)
    {
        for (var date = command.StartAtSchedule.Date; date <= command.EndAtSchedule.Date; date = date.AddDays(1))
        {
            if (date.IsDayOffAt(scheduler.DaysOff)) continue;
            AddAppointmentToSchedule(command, date, scheduler);
        }
    }

    private static void AddDaysOffToSchedule(CreateSchedulerCommand command, Domain.Entities.Scheduler scheduler)
    {
        foreach (var dayOffCommand in command.DaysOff)
        {
            DayOff dayOff = dayOffCommand;
            dayOff.SchedulerId = scheduler.Id;
            scheduler.AddDayOff(dayOff);
        }
    }

    private static void AddAppointmentToSchedule(CreateSchedulerCommand command, DateTime date,
        Domain.Entities.Scheduler scheduler)
    {
        var startAt = date.IsWeekday() ? command.StartAtWeekday : command.StartAtWeekend;
        var endAt = date.IsWeekday() ? command.EndAtWeekday : command.EndAtWeekend;

        for (var hour = startAt; hour <= endAt; hour = hour.Add(TimeSpan.FromMinutes(command.Duration)))
        {
            var appointment = new Appointment(date.Add(hour), command.Duration)
            {
                SchedulerId = scheduler.Id,
            };
            scheduler.AddAppointment(appointment);
        }
    }
}