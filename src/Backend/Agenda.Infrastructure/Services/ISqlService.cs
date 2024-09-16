using Microsoft.Data.SqlClient;

namespace Agenda.Infrastructure.Services;

public interface ISqlService
{
    Task<SqlDataReader> ExecuteSelectQueryAsync(string query);
}