namespace ContractService.UnitTest._Builders.Requests;

public class SignContractRequestBuilder : BaseBuilder<SignContractRequest>
{
    private Guid IdProposal;
    private DateTime SignAt;

    public SignContractRequestBuilder()
    {
        IdProposal = Guid.NewGuid();
    }

    public SignContractRequestBuilder WithSignAt(DateTime startAt)
    {
        SignAt = startAt;
        return this;
    }

    public override SignContractRequest Build()
    {
        return new SignContractRequest()
        {
            IdProposal = IdProposal,
            SignAt = SignAt
        };
    }
}
