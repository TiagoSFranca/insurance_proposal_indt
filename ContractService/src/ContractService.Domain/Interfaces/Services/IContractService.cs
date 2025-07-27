namespace ContractService.Domain.Interfaces.Services;

public interface IContractService
{
    Task<Result<Guid>> Create(CreateContractRequest request);

    Task<Result<PageResponse<ContractBriefResponse>>> Search(SearchContractRequest searchRequest, PageRequest page);
}
