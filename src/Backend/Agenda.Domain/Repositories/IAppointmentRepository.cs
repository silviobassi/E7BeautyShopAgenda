using Agenda.Domain.Entities;

namespace Agenda.Domain.Repositories;

public interface IAppointmentRepository
{
    Task AddRangeAsync(IList<Appointment> appointments);
}