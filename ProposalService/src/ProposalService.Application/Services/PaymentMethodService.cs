using Microsoft.EntityFrameworkCore;

namespace ProposalService.Application.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IProposalContext _context;

    public PaymentMethodService(IProposalContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentMethodResponse>> ListAll()
        => await _context
        .PaymentMethods
        .Select(e => new PaymentMethodResponse(e.Id, e.Name))
        .ToListAsync();
}
