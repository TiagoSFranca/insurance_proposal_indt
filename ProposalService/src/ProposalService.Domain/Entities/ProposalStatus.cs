using System.Collections.ObjectModel;

namespace ProposalService.Domain.Entities;

public class ProposalStatus : DbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ProposalStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ProposalStatus Analyzing = new((int)EProposalStatus.Analyzing, "Em análise");
    public static ProposalStatus Aproved = new((int)EProposalStatus.Aproved, "Aprovada");
    public static ProposalStatus Rejected = new((int)EProposalStatus.Rejected, "Rejeitada");

    public static ReadOnlyCollection<ProposalStatus> Seeds => new List<ProposalStatus>()
    {
        Analyzing,
        Aproved,
        Rejected,
    }.AsReadOnly();
}
