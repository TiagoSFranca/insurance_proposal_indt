using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProposalService.Application.Extensions;

namespace ProposalService.Application.Services;

public class ProposalService : IProposalService
{
    private readonly IProposalContext _context;
    private readonly IValidator<CreateProposalRequest> _createValidator;
    private readonly ILogger<ProposalService> _logger;

    public ProposalService(IProposalContext context, IValidator<CreateProposalRequest> createValidator, ILogger<ProposalService> logger)
    {
        _context = context;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task<Result<Guid>> Create(CreateProposalRequest request)
    {
        try
        {
            var result = _createValidator.Validate(request);

            var validationErrors = result.GetMessages();

            if (validationErrors.Count != 0) return Result<Guid>.Error(validationErrors);

            var existsInsuranceType = await _context
                .InsuranceTypes
                .Where(e => e.Id == request.IdInsuranceType)
                .AnyAsync();

            if(!existsInsuranceType) return Result<Guid>.Error("Tipo de seguro não encontrado");

            var existsPaymentMethod = await _context
                .PaymentMethods
                .Where(e => e.Id == request.IdPaymentMethod)
                .AnyAsync();

            if (!existsPaymentMethod) return Result<Guid>.Error("Forma de pagamento não encontrado");

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
