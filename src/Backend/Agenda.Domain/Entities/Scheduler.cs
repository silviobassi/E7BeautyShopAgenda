using Agenda.Domain.Errors;
using static Agenda.Domain.Errors.ErrorMessages;
using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Entities;

public class Scheduler : Entity
{
    public IEnumerable<DayOff> DaysOff { get; private set; } = [];
    public IEnumerable<Appointment> Appointments { get; private set; } = [];
    public long ProfessionalId { get; private set; }

    public Scheduler( long professionalId)
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
        if (DaysOff.ToList().Exists(dayOff => dayOff.DayOnWeek == appointment.AppointmentHours.DayOfWeek))
            return Result.Fail(APPOINTMENT_HOUR_CANNOT_DAY_OFF, AppointmentHourCannotBeOnDayOffCode);
        
        Appointments = Appointments.Append(appointment);
        return Result.Ok();
    }
}