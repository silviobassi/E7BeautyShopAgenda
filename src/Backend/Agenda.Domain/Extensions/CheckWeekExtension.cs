using Agenda.Domain.Entities;

namespace Agenda.Domain.Extensions;

public static class CheckWeekExtension
{
    public static bool IsDayOffAt(this DateTime appointmentDate, IList<DayOff> daysOff)
        => daysOff.ToList().Exists(dayOff => dayOff.DayOnWeek == appointmentDate.DayOfWeek);

    public static bool IsWeekday(this DateTime appointmentDate) => !IsWeekend(appointmentDate);
    
    private static bool IsWeekend(DateTime appointmentDate) =>
        appointmentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

}