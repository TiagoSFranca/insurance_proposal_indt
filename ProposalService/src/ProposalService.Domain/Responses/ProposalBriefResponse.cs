namespace ProposalService.Domain.Responses;

public record ProposalBriefResponse(
    Guid Id,
    Guid IdClient,
    int IdStatus,
    int IdInsuranceType,
    int IdPaymentMethod,
    DateOnly StartAt,
    DateOnly EndAt,
    DateTime CreatedAt,
    ProposalStatusResponse Status,
    InsuranceTypeResponse InsuranceType,
    PaymentMethodResponse PaymentMethod);
