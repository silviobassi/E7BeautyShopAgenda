namespace Agenda.Domain.Errors;

public static class ErrorMessages
{
    public const string ThereIsClientScheduledMessage = "Não há um cliente agendado para este horário";
    public const int ThereIsClientScheduledCode = 2;
    
    public const string NoClientScheduledMessage = "Não há um cliente agendado para este horário";
    public const int NoClientScheduledCode = 2;

    public const string LessThanTwoHoursBeforeMessage =
        "O horário não pode ser cancelado com menos de 2 horas de antecedência";
    public const int LessThanTwoHoursBeforeCode = 3;
}