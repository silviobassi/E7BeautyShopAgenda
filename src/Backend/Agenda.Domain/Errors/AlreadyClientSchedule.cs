using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public sealed record AlreadyClientSchedule()
    : DomainError(ALREADY_CLIENT_SCHEDULE, ErrorType.BusinessRule, nameof(AlreadyClientSchedule));