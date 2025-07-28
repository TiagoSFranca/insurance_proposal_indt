using ContractService.Domain.Responses.External;

namespace ContractService.Domain.Responses;

public record ContractResponse(
    Guid Id,
    Guid IdProposal,
    DateTime SignAt,
    DateTime CreatedAt)
{
    public ProposalResponse? Proposal { get; set; }
}
