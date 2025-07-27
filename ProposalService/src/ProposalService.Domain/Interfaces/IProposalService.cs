namespace ProposalService.Domain.Interfaces;

public interface IProposalService
{
    Task<Result<Guid>> Create(CreateProposalRequest request);

    Task<Result<PageResponse<ProposalBriefResponse>>> Search(SearchProposalRequest searchRequest, PageRequest page);
}
