namespace ProposalService.Domain.Interfaces;

public interface IProposalService
{
    Task<Result<Guid>> Create(CreateProposalRequest request);
}
