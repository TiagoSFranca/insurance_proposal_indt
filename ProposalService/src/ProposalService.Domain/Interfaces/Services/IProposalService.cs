namespace ProposalService.Domain.Interfaces.Services;

public interface IProposalService
{
    Task<Result<Guid>> Create(CreateProposalRequest request);

    Task<Result<PageResponse<ProposalBriefResponse>>> Search(SearchProposalRequest searchRequest, PageRequest page);

    Task<ProposalResponse?> Get(Guid id);

    Task<Result> UpdateStatus(Guid id, int idStatus);
}
