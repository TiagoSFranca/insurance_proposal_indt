namespace ContractService.Domain.Responses;

public record ContractResponse(
    Guid Id,
    Guid IdClient,
    int IdStatus,
    int IdInsuranceType,
    int IdPaymentMethod,
    decimal Premium,
    string? Notes,
    DateOnly StartAt,
    DateOnly EndAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
