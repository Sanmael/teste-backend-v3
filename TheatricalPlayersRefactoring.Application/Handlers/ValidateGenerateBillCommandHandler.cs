using FluentValidation;
using MediatR;
using TheatricalPlayersRefactoring.Application.Commands;

namespace TheatricalPlayersRefactoring.Application.Handlers;

public class ValidateGenerateBillCommandBehavior : IPipelineBehavior<GenerateBillCommand, GenerateBillResult>
{
    private readonly IValidator<GenerateBillCommand> _validator;

    public ValidateGenerateBillCommandBehavior(IValidator<GenerateBillCommand> validator)
    {
        _validator = validator;
    }

    public async Task<GenerateBillResult> Handle(
        GenerateBillCommand command,
        RequestHandlerDelegate<GenerateBillResult> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await next();
    }
}
