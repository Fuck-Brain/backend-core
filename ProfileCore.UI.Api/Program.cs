using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.OpenApi;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using ProfileCore.UI.Api.EndPoints;
using Serilog;
using Serilog.Events;

//Configure logging
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

//Create builder
var builder = WebApplication.CreateBuilder(args);

//Host logging  
builder.Host.UseSerilog((ctx, services, cfg) => cfg
	.ReadFrom.Configuration(ctx.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext()
	.WriteTo.Console()
);
	
// Controllers
builder.Services.AddProblemDetails();
	
// Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add definitions
var assembly = typeof(Program).Assembly;
await builder.AddApplicationDefinitions(assembly);
	
// Health checks
builder.Services.AddHealthChecks();
	
//Create web application
var app = builder.Build();
	
app.UseExceptionHandler();
	
//Use definitions
await app.UseApplicationDefinitions();

//Use logging
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
	IdentityModelEventSource.ShowPII = true;
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
	
app.UseExceptionHandler(errorApp => 
	errorApp.Run(async context =>
	{
		await Results.Problem().ExecuteAsync(context);
	}));

var api = app.MapGroup("/api");

api.MapGroup("/auth").MapAuthEndpoints();
api.MapGroup("/profile").MapProfileEndpoints();
api.MapGroup("/companies").MapCompanyEndpoints();

app.MapHealthChecks("/health");
app.MapGet("/throw", (_) => throw new Exception("Test exception"));

//Run app
await app.RunAsync();

await Log.CloseAndFlushAsync();
	
return 0;