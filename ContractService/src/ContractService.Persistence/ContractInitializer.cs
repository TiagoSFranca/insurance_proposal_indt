using Microsoft.Extensions.Logging;

namespace ContractService.Persistence;

public class ContractInitializer
{
    private readonly IContractContext _context;
    private readonly ILogger<ContractInitializer> _logger;

    public ContractInitializer(IContractContext context, ILogger<ContractInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Initialize()
    {
        try
        {
            _logger.LogInformation("Applying migration...");

            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while applying migration in database.");
            throw new InitializationException(ex);
        }
        finally
        {
            _logger.LogInformation("Finish of applying migration.");
        }
    }
}
