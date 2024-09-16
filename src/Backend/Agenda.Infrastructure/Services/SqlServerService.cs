using Agenda.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Agenda.Infrastructure.Services;

public class SqlServerService(IConfiguration configuration, ILogger<SqlServerService> logger) : ISqlService
{
    private readonly string? _connectionString = configuration.ConnectionString();
    public async Task<SqlDataReader> ExecuteSelectQueryAsync(string query)
    {
        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using SqlCommand command = new(query, connection);
        var reader = await command.ExecuteReaderAsync();

        return reader; // Para comandos SELECT
    }
}