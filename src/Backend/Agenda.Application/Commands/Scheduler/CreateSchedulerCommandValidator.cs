using Agenda.Communication.Commands.Scheduler;
using FluentValidation;

namespace Agenda.Application.Commands.Scheduler;

public class CreateSchedulerCommandValidator : AbstractValidator<CreateSchedulerCommand>
{
    public CreateSchedulerCommandValidator()
    {
        RuleFor(s => s.StartAtSchedule)
            .NotNull()
            .WithMessage("Start at schedule is required");

        RuleFor(s => s.EndAtSchedule)
            .NotEmpty()
            .WithMessage("Start at schedule is required");
        
        RuleFor(s => s.StartAtWeekday)
            .NotEmpty()
            .WithMessage("Start at weekday is required");

        RuleFor(s => s.EndAtWeekday)
            .NotEmpty()
            .WithMessage("End at weekday is required");

        RuleFor(s => s.StartAtWeekend)
            .NotEmpty()
            .WithMessage("Start at weekend is required");

        RuleFor(s => s.EndAtWeekend)
            .NotEmpty()
            .WithMessage("End at weekend is required");

        RuleFor(s => s.Duration)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than 0");

        RuleFor(s => s.DaysOff)
            .ForEach(dayOff => dayOff
                .Must(d => Enum.IsDefined(typeof(DayOfWeek), d.DayOnWeek))
                .WithMessage("Invalid day of the week in days off"));
        
        RuleFor(s => s.ProfessionalId)
            .NotEmpty()
            .WithMessage("Professional id is required");
    }
    
}