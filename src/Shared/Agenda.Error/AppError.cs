namespace Agenda.Error;

public record AppError(IList<string> errorsMessages, ErrorType ErrorType, string ErrorTypeName);