using ContractService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContractService.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractContext _context;
    private readonly IProposalRepository _proposalRepository;
    private readonly ILogger<ContractService> _logger;

    public ContractService(IContractContext context, IProposalRepository proposalRepository, ILogger<ContractService> logger)
    {
        _context = context;
        _proposalRepository = proposalRepository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Sign(SignContractRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            var proposal = await _proposalRepository.GetById(request.IdProposal);

            if (!proposal.IsSuccess) return Result<Guid>.Error(proposal.Messages.ToList());

            var entity = Contract.Create(
                request.IdProposal,
                request.SignAt);

            _context.Contracts.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error while creating contract");

            return Result<Guid>.Error(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating contract");

            return Result<Guid>.Error(ex);
        }
    }

    public async Task<Result<PageResponse<ContractBriefResponse>>> Search(SearchContractRequest request, PageRequest page)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = _context.Contracts.AsQueryable();

        if (page is null)
            page = PageRequest.First();

        if (request.Id.HasValue)
            query = query.Where(e => e.Id == request.Id);

        var select = query.Select(e => new ContractBriefResponse(
            e.Id,
            e.IdProposal,
            e.SignAt,
            e.CreatedAt));

        var result = await PaginationHelper.Paginate(select, page);

        return result;
    }

    public async Task<ContractResponse?> Get(Guid id)
    {
        var result = await _context
            .Contracts
            .Where(e => e.Id == id)
            .Select(e => new ContractResponse(
                e.Id,
                e.IdProposal,
                e.SignAt,
                e.CreatedAt))
            .FirstOrDefaultAsync();

        if (result is not null)
        {
            var proposal = await _proposalRepository.GetById(result.IdProposal);

            if (!proposal.IsSuccess)
                _logger.LogWarning("Proposal with id {Id} not found", result.IdProposal);
            else
                result.Proposal = proposal.Value;
        }

        return result;
    }
}
