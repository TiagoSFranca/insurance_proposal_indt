namespace ContractService.Domain.Responses;

public record ContractBriefResponse(
    Guid Id,
    Guid IdClient,
    int IdStatus,
    int IdInsuranceType,
    int IdPaymentMethod,
    DateOnly StartAt,
    DateOnly EndAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
