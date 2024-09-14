namespace Agenda.Domain.Errors;

public record DomainError(string Detail, ErrorType ErrorType, string ErrorTypeName);