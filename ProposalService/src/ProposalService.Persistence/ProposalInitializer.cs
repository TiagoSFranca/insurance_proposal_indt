using Microsoft.Extensions.Logging;

namespace ProposalService.Persistence;

public class ProposalInitializer
{
    private readonly IProposalContext _context;
    private readonly ILogger<ProposalInitializer> _logger;

    public ProposalInitializer(IProposalContext context, ILogger<ProposalInitializer> logger)
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

    public async Task Seed()
    {
        try
        {
            _logger.LogInformation("Seeding data...");

            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw new InitializationException(ex);
        }
        finally
        {
            _logger.LogInformation("Finish of seeding data in db.");
        }
    }

    private async Task TrySeedAsync()
    {
        await SeedInsuranceType();
        await SeedPaymentMethod();
        await SeedProposalStatus();

        await _context.SaveChangesAsync();
    }

    private async Task SeedInsuranceType()
    {
        if (await _context.InsuranceTypes.AnyAsync())
            return;

        _logger.LogDebug("Seeding {Name}...", nameof(InsuranceType));

        _context.InsuranceTypes.AddRange(InsuranceType.Seeds);
    }

    private async Task SeedPaymentMethod()
    {
        if (await _context.PaymentMethods.AnyAsync())
            return;

        _logger.LogDebug("Seeding {Name}...", nameof(PaymentMethod));

        _context.PaymentMethods.AddRange(PaymentMethod.Seeds);
    }

    private async Task SeedProposalStatus()
    {
        if (await _context.ProposalStatuses.AnyAsync())
            return;

        _logger.LogDebug("Seeding {Name}...", nameof(ProposalStatus));

        _context.ProposalStatuses.AddRange(ProposalStatus.Seeds);
    }
}
