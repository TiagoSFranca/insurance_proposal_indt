namespace ProposalService.UnitTest._Builders.Entities;

public class InsuranceTypeBuilder : BaseBuilder<InsuranceType>
{
    private int Id;
    private string Name;

    public InsuranceTypeBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public override InsuranceType Build()
    {
        return new InsuranceType(Id, Name);
    }
}
