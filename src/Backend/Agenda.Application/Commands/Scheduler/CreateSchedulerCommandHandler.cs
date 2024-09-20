﻿using Agenda.Application.Extensions;
using Agenda.Application.Services;
using Agenda.Communication.Commands.Scheduler;
using Agenda.Domain.Extensions;
using Agenda.Domain.Repositories;
using Agenda.Error;
using OneOf;

namespace Agenda.Application.Commands.Scheduler;

public class CreateSchedulerCommandHandler(
    ISchedulerRepository schedulerRepository,
    IAppointmentRepository appointmentRepository,
    IDayOffRepository dayOffRepository,
    IUnitOfWork unitOfWork
)
    : ICreateSchedulerCommandHandler
{
    /// <summary>
    /// Handles the creation of a scheduler based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the scheduler details.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// either a <see cref="CreateSchedulerCommandResult"/> if the operation is successful,
    /// or an <see cref="AppError"/> if there is an error.
    /// </returns>
    public async Task<OneOf<CreateSchedulerCommandResult, AppError>> Handle(CreateSchedulerCommand command)
    {
        var result = await ValidateAsync(command);
        if (result.IsError()) return result.GetError();

        var processor = new SchedulerProcessorService(command);
        processor.Process();

        await schedulerRepository.CreateAsync(processor.Schedule);
        unitOfWork.AutoDetectChangesEnabled(false);

        await dayOffRepository.AddRangeAsync(processor.DaysOff);
        await appointmentRepository.AddRangeAsync(processor.Appointments);
        unitOfWork.AutoDetectChangesEnabled(true);

        await unitOfWork.CommitAsync();
        return new CreateSchedulerCommandResult(processor.ScheduleId);
    }

    private static async Task<OneOf<bool, AppError>> ValidateAsync(CreateSchedulerCommand command)
    {
        var validator = new CreateSchedulerCommandValidator();
        var result = await validator.ValidateAsync(command);

        if (result.IsValid.IsTrue()) return true;
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        return new ErrorOnValidation(errorMessages);
    }
}