namespace Agenda.Domain.ValueObjects;

public class Weekend(TimeSpan startAt, TimeSpan endAt)
{
    public TimeSpan StartAt { get; private set; } = startAt;
    public TimeSpan EndAt { get; private set; } = endAt;
}