namespace Agenda.Error;

public sealed record ErrorOnValidation(IList<string> errorsMessages)
    : AppError(errorsMessages, ErrorType.Validation, nameof(ErrorOnValidation));