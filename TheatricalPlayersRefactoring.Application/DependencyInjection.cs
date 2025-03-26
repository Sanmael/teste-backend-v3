using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoring.Application.Factories;
using TheatricalPlayersRefactoring.Application.Handlers;
using TheatricalPlayersRefactoring.Application.Validation;

namespace TheatricalPlayersRefactoring.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GenerateBillCommandHandler>();
        services.AddScoped<GetBillQueryHandler>();        
        services.AddSingleton<IStatementFactoryProvider, StatementFactoryProvider>();
        services.AddValidatorsFromAssemblyContaining<GenerateBillCommandValidator>();
        return services;
    }
}