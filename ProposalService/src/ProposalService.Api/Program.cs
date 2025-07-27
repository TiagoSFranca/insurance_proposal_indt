using FluentValidation;
using ProposalService.Api.Extensions;
using ProposalService.Application.Extensions;
using ProposalService.Domain.Settings;
using ProposalService.Persistence.Extensions;
using Serilog;
using System.Globalization;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{EnvConstants.AspNetCoreEnvironment()}.json", true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
        .Configuration(configuration)
    .CreateLogger();

Log.Debug("Starting application with timezone {@tz}...", TimeZoneInfo.Local);

try
{
    ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

    EnvConstants.ValidateEnvs();

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder
        .Services
        .AddApi()
        .AddApplication()
        .AddPersistence();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        await app.InitializeDatabase();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseSerilogRequestLogging();

    app.UseHealthChecksEndpoints();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}