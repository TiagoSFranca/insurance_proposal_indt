using ContractService.Domain.Responses.External;

namespace ContractService.Domain.Interfaces.Repositories;

public interface IProposalRepository
{
    Task<Result<ProposalResponse>> GetById(Guid id);
}
