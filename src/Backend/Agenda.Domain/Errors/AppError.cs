namespace Agenda.Domain.Errors;

public record AppError(string Detail, ErrorType ErrorType, string ErrorTypeName);