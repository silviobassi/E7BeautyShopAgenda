namespace Agenda.Domain.Errors;

public record AppointmentLessThanTwoHours()
    : AppError(ResourceMessageError.LESS_THAN_TWO_HOURS, ErrorType.BusinessRule, nameof(AppointmentLessThanTwoHours));