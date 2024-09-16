using System.Reflection;
using Agenda.Application.Commands.Scheduler;
using Agenda.Domain.Repositories;
using Agenda.Infrastructure.DataAccess;
using Agenda.Infrastructure.DataAccess.Repositories;
using Agenda.Infrastructure.Extensions;
using Agenda.Infrastructure.Services;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        if (configuration.IsTest()) return;

        AddDbContext(services, configuration);
        AddDatabase(services);
        AddFluentMigrator(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AgendaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddDatabase(IServiceCollection services) =>
        services.AddSingleton<ISqlService, SqlServerService>();

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("Agenda.Infrastructure")).For.All());
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISchedulerRepository, SchedulerRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDayOffRepository, DayOffRepository>();
    }
}