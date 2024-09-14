namespace Agenda.Domain.Errors;

public record AlreadyClientSchedule()
    : AppError(ResourceMessageError.ALREADY_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(AlreadyClientSchedule));