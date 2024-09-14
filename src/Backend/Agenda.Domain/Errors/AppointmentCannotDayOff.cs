using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public record AppointmentCannotDayOff()
    : AppError(APPOINTMENT_CANNOT_DAY_OFF, ErrorType.BusinessRule, nameof(AppointmentCannotDayOff));