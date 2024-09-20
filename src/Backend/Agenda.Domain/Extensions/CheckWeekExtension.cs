using Agenda.Domain.Entities;

namespace Agenda.Domain.Extensions;

public static class CheckWeekExtension
{
    public static bool IsDayOffAt(this DateOnly appointmentDate, IList<DayOff> daysOff)
        => daysOff.ToList().Exists(dayOff => dayOff.DayOnWeek == appointmentDate.DayOfWeek);

    public static bool IsWeekday(this DateOnly appointmentDate) => !IsWeekend(appointmentDate);
    
    private static bool IsWeekend(DateOnly appointmentDate) =>
        appointmentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

}