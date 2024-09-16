using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.DataAccess;

public class AgendaDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Scheduler> Schedulers { get; init; }
    public DbSet<Appointment> Appointments { get; init; }
    public DbSet<DayOff> DaysOff { get; init; }
    public DbSet<Catalog> Catalogs { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgendaDbContext).Assembly);
    }
}