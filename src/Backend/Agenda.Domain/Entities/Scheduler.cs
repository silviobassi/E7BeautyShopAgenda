using Agenda.Domain.Errors;
using static Agenda.Domain.Errors.Result;

namespace Agenda.Domain.Entities;

public class Scheduler : Entity
{
    public IEnumerable<DayOff> DaysOff { get; private set; } = [];
    public IEnumerable<Appointment> Appointments { get; private set; } = [];
    public long ProfessionalId { get; private set; }

    public Scheduler(long professionalId)
    {
        ProfessionalId = professionalId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(long id, long newProfessionalId)
    {
        Id = id;
        ProfessionalId = newProfessionalId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AddDayOff(DayOff dayOff) => DaysOff = DaysOff.Append(dayOff);

    public Result AddAppointment(Appointment appointment)
    {
        var appointmentHoursDayOfWeek = appointment.AppointmentHours.DayOfWeek;
        if (DaysOff.ToList().Exists(dayOff => dayOff.DayOnWeek == appointmentHoursDayOfWeek))
            return new AppointmentCannotDayOff();

        Appointments = Appointments.Append(appointment);
        return Ok();
    }
}