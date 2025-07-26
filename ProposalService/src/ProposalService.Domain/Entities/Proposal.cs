using ProposalService.Domain.Enums;

namespace ProposalService.Domain.Entities;

public class Proposal : DbEntity
{
    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public int IdStatus { get; private set; }
    public int IdInsuranceType { get; private set; }
    public int IdPaymentMethod { get; private set; }
    public decimal Premium { get; set; }
    public string? Notes { get; set; }
    public DateOnly StartAt { get; set; }
    public DateOnly EndAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ProposalStatus Status { get; private set; }
    public virtual InsuranceType InsuranceType { get; private set; }
    public virtual PaymentMethod PaymentMethod { get; private set; }
    public virtual Client Client { get; private set; }

    private Proposal() { }

    public static Proposal Create(
        Guid idClient,
        int idInsuranceType,
        int idPaymentMethod,
        decimal premium,
        string? notes,
        DateOnly startAt,
        DateOnly endAt)
    {
        return new()
        {
            IdClient = idClient,
            IdStatus = (int)EProposalStatus.Analyzing,
            IdInsuranceType = idInsuranceType,
            IdPaymentMethod = idPaymentMethod,
            Premium = premium,
            Notes = notes,
            StartAt = startAt,
            EndAt = endAt,
            CreatedAt = DateTime.Now
        };
    }
}

public class InsuranceType : DbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PaymentMethod : DbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Client : DbEntity
{
    public int Id { get; set; }
}
