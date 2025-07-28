using ContractService.Domain.Settings;
using ContractService.Api.Extensions;
using ContractService.Application.Extensions;
using ContractService.Persistence.Extensions;
using Serilog;

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