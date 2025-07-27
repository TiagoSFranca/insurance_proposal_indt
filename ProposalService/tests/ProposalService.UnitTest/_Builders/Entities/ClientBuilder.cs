namespace ProposalService.UnitTest._Builders.Entities;

public class ClientBuilder : BaseBuilder<Client>
{
    private Guid Id;
    private readonly DateTime CreatedAt;

    public ClientBuilder()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public ClientBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public override Client Build()
    {
        return new Client(Id, CreatedAt);
    }
}
