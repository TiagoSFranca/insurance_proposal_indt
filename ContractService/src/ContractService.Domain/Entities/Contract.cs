namespace ContractService.Domain.Entities;

public class Contract : DbEntity
{
    public Guid Id { get; private set; }
    public Guid IdProposal { get; private set; }
    public DateTime SignAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Contract Create(Guid idProposal, DateTime signAt)
    {
        return new Contract()
        {
            IdProposal = idProposal,
            SignAt = signAt,
            CreatedAt = DateTime.Now
        };
    }
}
