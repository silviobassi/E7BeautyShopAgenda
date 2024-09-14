using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public record AppointmentLessThanTwoHours()
    : DomainError(LESS_THAN_TWO_HOURS, ErrorType.BusinessRule, nameof(AppointmentLessThanTwoHours));