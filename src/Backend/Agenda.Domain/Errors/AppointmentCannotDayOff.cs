namespace Agenda.Domain.Errors;

public record AppointmentCannotDayOff()
    : AppError(ResourceMessageError.APPOINTMENT_CANNOT_DAY_OFF, ErrorType.BusinessRule, nameof(AppointmentCannotDayOff));