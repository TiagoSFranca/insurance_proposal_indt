namespace ProposalService.Domain.Requests;

public class SearchProposalRequest
{
    public List<Guid>? Ids { get; set; }
    public List<int>? IdStatuses { get; set; }
}
