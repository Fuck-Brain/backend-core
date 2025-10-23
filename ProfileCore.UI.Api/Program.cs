using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using ProfileCore.Application.Services;
using ProfileCore.Application.Services.Interfaces;
using ProfileCore.Infrastructure.Database;
using ProfileCore.UI.Api.EndPoints;
using Serilog;
using Serilog.Events;

IdentityModelEventSource.ShowPII = true;

//Configure logging
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

//Create builder
var builder = WebApplication.CreateBuilder(args);

//JWT
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");
        var issuer     = jwt["Issuer"];
        var audience   = jwt["Audience"];
        var secret     = jwt["SecretKey"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = issuer,
            ValidAudience            = audience,
			IssuerSigningKey = string.IsNullOrWhiteSpace(secret)
				? null
				: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

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
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	var scheme = new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,     
		Scheme = "bearer",                  
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Введите токен вида: Bearer {token}",
		Reference = new OpenApiReference
		{
			Type = ReferenceType.SecurityScheme,
			Id = "Bearer"
		}
	};

	c.AddSecurityDefinition("Bearer", scheme);
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ scheme, Array.Empty<string>() }
	});
});

//Add definitions
var assembly = typeof(Program).Assembly;
await builder.AddApplicationDefinitions(assembly);
	
// Health checks
builder.Services.AddHealthChecks();
	
//Create web application
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var seeder = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
	await seeder.SeedAsync(); 
}
	
app.UseExceptionHandler();
app.UseStatusCodePages();
	
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

app.UseAuthentication();
app.UseAuthorization();
	
/*app.UseExceptionHandler(errorApp => 
	errorApp.Run(async context =>
	{
		await Results.Problem().ExecuteAsync(context);
	}));*/

var api = app.MapGroup("/api");

api.MapGroup("/auth").MapAuthEndpoints();
api.MapGroup("/profile").MapProfileEndpoints();
api.MapGroup("/companies").MapCompanyEndpoints();
api.MapGroup("/plugins").MapPluginEndpoints();


app.MapHealthChecks("/health");
app.MapGet("/throw", (_) => throw new Exception("Test exception"));

//Run app
await app.RunAsync();

await Log.CloseAndFlushAsync();
	
return 0;