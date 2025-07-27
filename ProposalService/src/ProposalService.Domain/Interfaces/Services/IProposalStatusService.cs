namespace ProposalService.Domain.Interfaces.Services;

public interface IProposalStatusService
{
    Task<List<ProposalStatusResponse>> ListAll();
}
