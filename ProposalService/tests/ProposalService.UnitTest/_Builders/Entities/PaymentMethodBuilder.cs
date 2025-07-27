namespace ProposalService.UnitTest._Builders.Entities;

public class PaymentMethodBuilder : BaseBuilder<PaymentMethod>
{
    private int Id;
    private string Name;

    public PaymentMethodBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public override PaymentMethod Build()
    {
        return new PaymentMethod(Id, Name);
    }
}
