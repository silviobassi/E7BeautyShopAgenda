using Agenda.Communication.Commands.Scheduler;
using Agenda.Error;
using OneOf;

namespace Agenda.Application.Commands.Scheduler;

public interface ICreateSchedulerCommandHandler
{
    Task<OneOf<CreateSchedulerCommandResult, AppError>> Handle(CreateSchedulerCommand command);
}