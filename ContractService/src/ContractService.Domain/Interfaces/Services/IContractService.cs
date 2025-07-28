namespace ContractService.Domain.Interfaces.Services;

public interface IContractService
{
    Task<Result<Guid>> Sign(SignContractRequest request);

    Task<Result<PageResponse<ContractBriefResponse>>> Search(SearchContractRequest searchRequest, PageRequest page);

    Task<ContractResponse?> Get(Guid id);
}
