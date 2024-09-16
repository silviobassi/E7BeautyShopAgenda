using Microsoft.Extensions.Configuration;

namespace Agenda.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTest(this IConfiguration configuration) => configuration.GetValue<bool>("InMemoryTest");
    
    public static string? ConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("DefaultConnection");
}