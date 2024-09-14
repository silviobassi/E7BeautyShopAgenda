namespace Agenda.Domain.Errors;

public class Result
{
    public bool Success { get; }

    public AppError? Error { get; }


    private Result(bool success, AppError? error)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok() => new(true, null);
    public static Result Fail(AppError error) => new(false, error);
}