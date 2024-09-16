namespace Agenda.Domain.Entities;

public class Scheduler : Entity
{
    public IList<DayOff> DaysOff { get; private set; } = [];
    public IList<Appointment> Appointments { get; private set; } = [];
    public long ProfessionalId { get; private set; }

    public Scheduler()
    {
    }

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

    public void AddDayOff(DayOff dayOff) => DaysOff.Add(dayOff);

    public void AddAppointment(Appointment appointment) => Appointments.Add(appointment);
}