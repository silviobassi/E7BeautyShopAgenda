using Microsoft.Data.SqlClient;

namespace Agenda.Infrastructure.Services;

public interface ISqlService
{
    Task ExecuteQuery(string query);
    Task<SqlDataReader> ExecuteSelectQuery(string query);
}