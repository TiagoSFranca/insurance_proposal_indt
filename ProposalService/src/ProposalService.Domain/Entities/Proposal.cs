namespace ProposalService.Domain.Entities;

public class Proposal : DbEntity
{
    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public int IdStatus { get; private set; }
    public int IdInsuranceType { get; private set; }
    public int IdPaymentMethod { get; private set; }
    public decimal Premium { get; private set; }
    public string? Notes { get; private set; }
    public DateOnly StartAt { get; private set; }
    public DateOnly EndAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

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

    public void UpdateStatus(int idStatus)
    {
        IdStatus = idStatus;
        UpdatedAt = DateTime.Now;
    }
}
