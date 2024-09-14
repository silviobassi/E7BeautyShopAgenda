namespace Agenda.Domain.Errors;

public class Result
{
    public bool Success { get; }
    public string ErrorMessage { get; }
    public int ErrorCode { get; }

    private Result(bool success, string errorMessage, int errorCode)
    {
        Success = success;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public static Result Ok() => new(true, string.Empty, 0);
    public static Result Fail(string message, int errorCode) => new(false, message, errorCode);
}