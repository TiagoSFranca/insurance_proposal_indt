namespace ProposalService.UnitTest._Builders.Entities;

public class InsuranceTypeBuilder : BaseBuilder<InsuranceType>
{
    private int Id;

    public InsuranceTypeBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public override InsuranceType Build()
    {
        return new InsuranceType()
        {
            Id = Id,
        };
    }
}
