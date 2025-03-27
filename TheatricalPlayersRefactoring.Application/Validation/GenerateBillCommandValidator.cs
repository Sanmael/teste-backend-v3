using FluentValidation;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Application.Validation;

public class GenerateBillCommandValidator : AbstractValidator<GenerateBillCommand>
{
    public GenerateBillCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Performances)
            .NotEmpty()
            .WithMessage("At least one performance is required");

        RuleForEach(x => x.Performances).SetValidator(new PerformanceRequestValidator());
    }
}

public class PerformanceRequestValidator : AbstractValidator<PerformanceRequest>
{
    public PerformanceRequestValidator()
    {
        RuleFor(x => x.PlayName)
            .NotEmpty()
            .MaximumLength(100);

        //RuleFor(x => x.Lines) TODO: Duvida, o limite é entre 1000 e 4000, mas está sendo arredondado o valor.
        //    .InclusiveBetween(1000, 4000)
        //    .WithMessage("'Lines' must be between 1000 and 4000");

        RuleFor(x => x.PlayType)
            .NotEmpty()
            .Must(BeValidPlayType)
            .WithMessage("Invalid play type. Allowed values are: Tragedy, Comedy, History");

        RuleFor(x => x.Audience)
            .GreaterThanOrEqualTo(0);
    }

    private bool BeValidPlayType(string playType)
    {
        return Enum.TryParse<PlayType>(playType, true,out _);
    }
}