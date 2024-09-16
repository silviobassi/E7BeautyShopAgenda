using Agenda.Application.Commands.Scheduler;
using Agenda.Communication.Commands.Scheduler;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers;

public class SchedulerController : E7BeautyShopAgendaController
{
    [HttpPost]
    public async Task<IActionResult> CreateScheduler([FromBody] CreateSchedulerCommand command,
        [FromServices] ICreateSchedulerCommandHandler commandHandler)
    {
        await commandHandler.Handle(command);
        return Ok();
    }
}