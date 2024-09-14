using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public sealed record AppointmentLessThanTwoHours()
    : DomainError(LESS_THAN_TWO_HOURS, ErrorType.BusinessRule, nameof(AppointmentLessThanTwoHours));