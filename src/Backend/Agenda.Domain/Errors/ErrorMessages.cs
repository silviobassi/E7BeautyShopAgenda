namespace Agenda.Domain.Errors;

public static class ErrorMessages
{
    public const string ClientScheduledMessage = "Não há um cliente agendado para este horário";
    public const int ClientScheduledCode = 2;
    
    public const string NoClientScheduledMessage = "Não há um cliente agendado para este horário";
    public const int NoClientScheduledCode = 2;

    public const string LessThanTwoHoursBeforeMessage =
        "O horário não pode ser cancelado com menos de 2 horas de antecedência";
    public const int LessThanTwoHoursBeforeCode = 3;
    
    public const string AppointmentHourCannotBeOnDayOffMessage = "Não pode haver horário de atendimento em dia de folga";
    public const int AppointmentHourCannotBeOnDayOffCode = 4;
}