using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;
using TheatricalPlayersRefactoring.Application.Factories;
using TheatricalPlayersRefactoring.Application.Handlers;
using TheatricalPlayersRefactoring.Application.Services;
using TheatricalPlayersRefactoring.Application.Validation;

namespace TheatricalPlayersRefactoring.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GenerateBillCommandHandler>();
        services.AddScoped<GetBillQueryHandler>();        
        services.AddScoped<IFileSystem, FileSystem>();        
        services.AddScoped<IFileBuilder, FileBuilder>();        
        services.AddSingleton<IStatementFactoryProvider, StatementFactoryProvider>();
        services.AddValidatorsFromAssemblyContaining<GenerateBillCommandValidator>();
        return services;
    }
}