using Agenda.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Agenda.Infrastructure.Services;

public class SqlServerService(IConfiguration configuration) : ISqlService
{
    private readonly string? _connectionString = configuration.ConnectionString();
    
    public async Task ExecuteQuery(string query)
    {
        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using SqlCommand command = new(query, connection);
        command.Prepare();
        await command.ExecuteNonQueryAsync(); // Para comandos como INSERT, UPDATE, DELETE
    }
    
    public async  Task<SqlDataReader> ExecuteSelectQuery(string query)
    {
        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using SqlCommand command = new(query, connection);
        var reader = await command.ExecuteReaderAsync();

        return reader; // Para comandos SELECT
    }
}