using Agenda.Domain.Entities;
using Agenda.Domain.Repositories;

namespace Agenda.Infrastructure.DataAccess.Repositories;

public class AppointmentRepository(AgendaDbContext dbContext) : IAppointmentRepository
{
    public async Task AddRangeAsync(IList<Appointment> appointments) =>
        await dbContext.Appointments.AddRangeAsync(appointments);
}