namespace Agenda.Domain.ValueObjects;

public class Weekday(TimeSpan startAt, TimeSpan endAt)
{
    public TimeSpan StartAt { get; private set; } = startAt;
    public TimeSpan EndAt { get; private set; } = endAt;
}