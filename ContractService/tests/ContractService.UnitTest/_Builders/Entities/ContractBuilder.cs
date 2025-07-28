namespace ContractService.UnitTest._Builders.Entities;

public class ContractBuilder : BaseBuilder<Contract>
{
    private Guid Id;
    private Guid IdProposal;
    private DateTime SignAt;

    public ContractBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    public ContractBuilder WithIdProposal(Guid idProposal)
    {
        IdProposal = idProposal;
        return this;
    }

    public override Contract Build()
    {
        var model = Contract.Create(IdProposal, SignAt);

        return model;
    }
}
