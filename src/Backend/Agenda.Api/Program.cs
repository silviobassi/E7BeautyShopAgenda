using Agenda.Api.Filters;
using Agenda.Application;
using Agenda.Infrastructure;
using Agenda.Infrastructure.Extensions;
using Agenda.Infrastructure.Migrations;
using StringConverter = Agenda.Api.Converters.StringConverter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(OneOfResultFilter)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    if (builder.Configuration.IsTest()) return;
    var connectionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DatabaseMigration.Migrate(connectionString!, serviceScope.ServiceProvider);
}

public partial class Program
{
    protected Program()
    {
    }
}