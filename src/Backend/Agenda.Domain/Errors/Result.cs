namespace Agenda.Domain.Errors;

public class Result
{
    public bool IsSuccess { get; }

    public DomainError? Error { get; }
    
    private Result(bool isSuccess, DomainError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static implicit operator Result(DomainError error) => Fail(error);
    private static Result Fail(DomainError error) => new(false, error);
}