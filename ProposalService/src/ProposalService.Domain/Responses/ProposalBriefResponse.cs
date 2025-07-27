namespace ProposalService.Domain.Responses;

public record ProposalBriefResponse(
    Guid Id,
    Guid IdClient,
    int IdInsuranceType,
    int IdPaymentMethod,
    DateOnly StartAt,
    DateOnly EndAt,
    DateTime CreatedAt);
