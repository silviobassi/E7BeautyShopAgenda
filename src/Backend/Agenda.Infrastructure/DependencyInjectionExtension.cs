using System.Reflection;
using Agenda.Infrastructure.Extensions;
using Agenda.Infrastructure.Services;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsTest()) return;

        AddDatabase(services);
        AddFluentMigrator(services, configuration);
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
}