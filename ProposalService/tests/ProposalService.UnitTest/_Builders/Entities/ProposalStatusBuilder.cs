namespace ProposalService.UnitTest._Builders.Entities;

public class ProposalStatusBuilder : BaseBuilder<ProposalStatus>
{
    private int Id;
    private string Name;

    public ProposalStatusBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public override ProposalStatus Build()
    {
        return new ProposalStatus(Id, Name);
    }
}
