namespace ContractService.UnitTest._Builders.Responses;

public class ProposalResponseBuilder : BaseBuilder<ProposalResponse>
{
    private Guid Id;
    private Guid IdClient;
    private int IdStatus;
    private int IdInsuranceType;
    private int IdPaymentMethod;
    private decimal Premium;
    private string? Notes;
    private DateOnly StartAt;
    private DateOnly EndAt;
    private DateTime CreatedAt;
    private DateTime? UpdatedAt;
    private ProposalStatusResponse Status;
    private InsuranceTypeResponse InsuranceType;
    private PaymentMethodResponse PaymentMethod;
    private ClientResponse Client;

    public ProposalResponseBuilder WithIdStatus(int idStatus)
    {
        IdStatus = idStatus;
        return this;
    }

    public override ProposalResponse Build()
    {
        return new ProposalResponse(
            Id,
            IdClient,
            IdStatus,
            IdInsuranceType,
            IdPaymentMethod,
            Premium,
            Notes,
            StartAt,
            EndAt,
            CreatedAt,
            UpdatedAt,
            Status,
            InsuranceType,
            PaymentMethod,
            Client);
    }
}
