namespace ProposalService.Domain.Requests;

public class CreateProposalRequest
{
    public Guid IdClient { get; set; }
    public int IdInsuranceType { get;  set; }
    public int IdPaymentMethod { get; set; }
    public decimal Premium { get; set; }
    public string? Notes { get; set; }
    public DateOnly StartAt { get; set; }
    public DateOnly EndAt { get; set; }
}
