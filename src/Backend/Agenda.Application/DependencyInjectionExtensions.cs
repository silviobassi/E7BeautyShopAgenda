using Agenda.Application.Commands.Scheduler;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddCommands(services);
    }

    private static void AddCommands(IServiceCollection services)
    {
        services.AddScoped<ICreateSchedulerCommandHandler, CreateSchedulerCommandHandler>();
    }
}