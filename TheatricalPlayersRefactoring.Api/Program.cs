using TheatricalPlayersRefactoring.Application;
using TheatricalPlayersRefactoring.Infrastructure;
using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoring.Api.Middleware;
using TheatricalPlayersRefactoring.Application.Commands;
using FluentValidation;
using MediatR;
using TheatricalPlayersRefactoring.Application.Validation;
using TheatricalPlayersRefactoring.Application.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GenerateBillCommand).Assembly);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Theatre Billing API", 
        Version = "v1",
        Description = "API for managing theatre billing and invoices"
    });
});
builder.Services.AddScoped<IValidator<GenerateBillCommand>, GenerateBillCommandValidator>();
builder.Services.AddScoped<IPipelineBehavior<GenerateBillCommand, GenerateBillResult>, ValidateGenerateBillCommandBehavior>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();