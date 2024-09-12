namespace Agenda.Domain.Errors;

public class Result
{
    public bool Success { get; }
    public string Message { get; }
    public int ErrorCode { get; }

    private Result(bool success, string message, int errorCode)
    {
        Success = success;
        Message = message;
        ErrorCode = errorCode;
    }

    public static Result Ok() => new(true, string.Empty, 0);
    public static Result Fail(string message, int errorCode) => new(false, message, errorCode);
}