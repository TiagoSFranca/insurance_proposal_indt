using Microsoft.EntityFrameworkCore;

namespace ProposalService.Application.Services;

public class ProposalStatusService : IProposalStatusService
{
    private readonly IProposalContext _context;

    public ProposalStatusService(IProposalContext context)
    {
        _context = context;
    }

    public async Task<List<ProposalStatusResponse>> ListAll()
        => await _context
        .ProposalStatuses
        .Select(e => new ProposalStatusResponse(e.Id, e.Name))
        .ToListAsync();
}
