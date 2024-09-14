using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public sealed record AppointmentCannotDayOff()
    : DomainError(APPOINTMENT_CANNOT_DAY_OFF, ErrorType.BusinessRule, nameof(AppointmentCannotDayOff));