namespace ProposalService.Domain.Entities;

public class Client : DbEntity
{
    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Client(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}
