using Agenda.Application.Commands.Scheduler;
using Agenda.Communication.Commands.Scheduler;
using Agenda.Error;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers;

public class SchedulerController : E7BeautyShopAgendaController
{
    [HttpPost]
    public async Task<IActionResult> CreateScheduler([FromBody] CreateSchedulerCommand command,
        [FromServices] ICreateSchedulerCommandHandler commandHandler)
    {
        var schedulerResult = await commandHandler.Handle(command);
        return schedulerResult.Match<IActionResult>(
            success => Created(string.Empty, success),
            error => new ObjectResult(error));
    }
}