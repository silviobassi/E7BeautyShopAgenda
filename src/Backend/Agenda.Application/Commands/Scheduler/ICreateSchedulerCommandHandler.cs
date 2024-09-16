using Agenda.Communication.Commands.Scheduler;

namespace Agenda.Application.Commands.Scheduler;

public interface ICreateSchedulerCommandHandler
{
    Task Handle(CreateSchedulerCommand command);
}