using Microsoft.EntityFrameworkCore;

namespace ProposalService.Application.Services;

public class InsuranceTypeService : IInsuranceTypeService
{
    private readonly IProposalContext _context;

    public InsuranceTypeService(IProposalContext context)
    {
        _context = context;
    }

    public async Task<List<InsuranceTypeResponse>> ListAll()
        => await _context
        .InsuranceTypes
        .Select(e => new InsuranceTypeResponse(e.Id, e.Name))
        .ToListAsync();
}
