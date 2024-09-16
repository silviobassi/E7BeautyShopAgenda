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

    public void AddAppointment(Appointment appointment)
    {
        if (IsDayOff(appointment)) return;
        Appointments = Appointments.Append(appointment);
    }

    private bool IsDayOff(Appointment appointment)
        => DaysOff.ToList().Exists(dayOff => dayOff.DayOnWeek == appointment.AppointmentHour.DayOfWeek);
}