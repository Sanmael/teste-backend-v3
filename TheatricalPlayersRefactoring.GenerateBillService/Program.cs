using TheatricalPlayersRefactoring.Application;
using TheatricalPlayersRefactoring.Infrastructure;
using TheatricalPlayersRefactoring.GenerateBillService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddTransient<BillGenerationWorker>();


var host = builder.Build();
host.Run();
