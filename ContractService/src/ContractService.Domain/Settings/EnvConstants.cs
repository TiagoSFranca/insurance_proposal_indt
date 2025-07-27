using System.Reflection;

namespace ContractService.Domain.Settings;

public static class EnvConstants
{
    private const string ENV_ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
    private const string ENV_DATABASE_CONNECTION_STRING = "DATABASE_CONNECTION_STRING";
    private const string ENV_USE_EF_MIGRATION = "USE_EF_MIGRATION";
    private const string ENV_SEED_DATABASE = "SEED_DATABASE";

    private static string? GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }

    private static string GetEnvironmentVariable(string name, string? defaultValue, bool errorIfEmpty = false)
    {
        var value = GetEnvironmentVariable(name);

        if (!string.IsNullOrWhiteSpace(value)) return value;

        if (errorIfEmpty) throw new InvalidCastException($"Env var {name} must contain value.");

        return defaultValue ?? string.Empty;
    }

    private static string GetRequiredEnvironmentVariable(string name)
    {
        return GetEnvironmentVariable(name, null, true);
    }

    public static string AspNetCoreEnvironment() => GetRequiredEnvironmentVariable(ENV_ASPNETCORE_ENVIRONMENT);
    public static string DatabaseConnectionString() => GetRequiredEnvironmentVariable(ENV_DATABASE_CONNECTION_STRING);
    public static bool UseEFMigration()
    {
        var result = GetEnvironmentVariable(ENV_USE_EF_MIGRATION);

        if (bool.TryParse(result, out bool useMigration))
            return useMigration;

        return useMigration;
    }
    public static bool SeedDatabase()
    {
        var result = GetEnvironmentVariable(ENV_SEED_DATABASE);

        if (bool.TryParse(result, out bool seed))
            return seed;

        return seed;
    }

    public static void ValidateEnvs()
    {
        var methodInfos = typeof(EnvConstants)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(e => e.ReturnType != typeof(void))
            .Where(e => e.GetParameters().Length > 0)
            .ToList();

        var errors = new List<string>();

        foreach (var item in methodInfos)
        {
            try
            {
                item.Invoke(null, null);
            }
            catch (Exception ex)
            {
                errors.Add(ex.InnerException?.Message ?? ex.Message);
            }
        }

        if (errors.Count > 0) throw new InvalidOperationException(string.Join("\n", errors));
    }
}
