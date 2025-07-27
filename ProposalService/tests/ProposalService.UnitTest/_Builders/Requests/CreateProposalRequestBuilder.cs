using Bogus;

namespace ProposalService.UnitTest._Builders.Requests;

public class CreateProposalRequestBuilder : BaseBuilder<CreateProposalRequest>
{
    private Guid IdClient;
    private int IdInsuranceType;
    private int IdPaymentMethod;
    private decimal Premium;
    private string? Notes;
    private DateOnly StartAt;
    private DateOnly EndAt;


    public CreateProposalRequestBuilder()
    {
        Faker faker = new Faker();

        IdInsuranceType = faker.Random.Int(1, 10);
        IdPaymentMethod = faker.Random.Int(1, 10);
    }

    public CreateProposalRequestBuilder WithPremium(decimal premium)
    {
        Premium = premium;
        return this;
    }

    public CreateProposalRequestBuilder WithStartAt(DateOnly startAt)
    {
        StartAt = startAt;
        return this;
    }
    public CreateProposalRequestBuilder WithEndAt(DateOnly endAt)
    {
        EndAt = endAt;
        return this;
    }

    public override CreateProposalRequest Build()
    {
        return new CreateProposalRequest()
        {
            StartAt = StartAt,
            EndAt = EndAt,
            Premium = Premium,
            IdInsuranceType = IdInsuranceType,
            IdPaymentMethod = IdPaymentMethod,
        };
    }
}
