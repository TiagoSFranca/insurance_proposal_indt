namespace ContractService.Domain.Responses;

public record ContractBriefResponse(
    Guid Id,
    Guid IdProposal,
    DateTime SignAt,
    DateTime CreatedAt);
