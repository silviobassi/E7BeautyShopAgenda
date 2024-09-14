using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public record AlreadyClientSchedule()
    : AppError(ALREADY_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(AlreadyClientSchedule));