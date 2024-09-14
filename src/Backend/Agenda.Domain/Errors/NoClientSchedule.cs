using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public record NoClientSchedule() : AppError(NO_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(NoClientSchedule));