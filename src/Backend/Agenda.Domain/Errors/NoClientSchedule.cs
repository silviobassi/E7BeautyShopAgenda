using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public record NoClientSchedule() : DomainError(NO_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(NoClientSchedule));