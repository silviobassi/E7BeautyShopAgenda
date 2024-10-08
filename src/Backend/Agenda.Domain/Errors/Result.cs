﻿namespace Agenda.Domain.Errors;

public class Result
{
    public bool Success { get; }

    public DomainError? Error { get; }
    
    private Result(bool success, DomainError? error)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok() => new(true, null);
    public static implicit operator Result(DomainError error) => Fail(error);
    private static Result Fail(DomainError error) => new(false, error);
}