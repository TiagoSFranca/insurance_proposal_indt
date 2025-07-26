using Microsoft.Extensions.Logging;

namespace ProposalService.Application.Services;

public class ProposalService : IProposalService
{
    private readonly IProposalContext _context;
    private readonly ILogger<ProposalService> _logger;

    public ProposalService(IProposalContext context, ILogger<ProposalService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Create(CreateProposalRequest request)
    {
        try
        {
            var entity = Proposal.Create(
                request.IdClient,
                request.IdInsuranceType,
                request.IdPaymentMethod,
                request.Premium,
                request.Notes,
                request.StartAt,
                request.EndAt);

            _context.Proposals.Add(entity);

            await _context.SaveChangesAsync();

            return Result<Guid>.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Error while creating proposal");

            return Result<Guid>.Error();
        }
    }
}
