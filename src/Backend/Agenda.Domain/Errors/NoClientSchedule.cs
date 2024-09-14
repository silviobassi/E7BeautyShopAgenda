namespace Agenda.Domain.Errors;

public record NoClientSchedule()
    : AppError(ResourceMessageError.NO_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(NoClientSchedule));