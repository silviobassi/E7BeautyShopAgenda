using static Agenda.Domain.Errors.ResourceMessageError;

namespace Agenda.Domain.Errors;

public sealed record CatalogNotFound()
    : DomainError(CATALOG_NOT_FOUND, ErrorType.BusinessRule, nameof(CatalogNotFound));