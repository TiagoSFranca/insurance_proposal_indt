namespace ContractService.Domain.Responses.External;

public record ProposalResponse(
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
    DateTime? UpdatedAt,
    ProposalStatusResponse Status,
    InsuranceTypeResponse InsuranceType,
    PaymentMethodResponse PaymentMethod,
    ClientResponse Client);
