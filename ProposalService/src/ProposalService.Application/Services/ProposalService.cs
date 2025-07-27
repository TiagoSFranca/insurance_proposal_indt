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
            ArgumentNullException.ThrowIfNull(request);

            var result = _createValidator.Validate(request);

            var validationErrors = result.GetMessages();

            if (validationErrors.Count != 0) return Result<Guid>.Error(validationErrors);

            var existsInsuranceType = await _context
                .InsuranceTypes
                .Where(e => e.Id == request.IdInsuranceType)
                .AnyAsync();

            if (!existsInsuranceType) return Result<Guid>.Error(Messages.InsuranceTypeNotFound);

            var existsPaymentMethod = await _context
                .PaymentMethods
                .Where(e => e.Id == request.IdPaymentMethod)
                .AnyAsync();

            if (!existsPaymentMethod) return Result<Guid>.Error(Messages.PaymentMethodNotFound);

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

            return entity.Id;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error while creating proposal");

            return Result<Guid>.Error(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating proposal");

            return Result<Guid>.Error(ex);
        }
    }

    public async Task<Result<PageResponse<ProposalBriefResponse>>> Search(SearchProposalRequest request, PageRequest page)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = _context.Proposals.AsQueryable();

        if (page is null)
            page = PageRequest.First();

        if (request.Id.HasValue)
            query = query.Where(e => e.Id == request.Id);

        var select = query.Select(e => new ProposalBriefResponse(e.Id, e.IdClient, e.IdInsuranceType, e.IdPaymentMethod, e.StartAt, e.EndAt, e.CreatedAt));

        var result = await PaginationHelper.Paginate(select, page);

        return result;
    }
}
