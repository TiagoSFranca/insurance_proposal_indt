using ProposalService.Domain.Enums;

namespace ProposalService.UnitTest._Builders.Entities;

public class ProposalBuilder : BaseBuilder<Proposal>
{
    private Guid Id;
    private Guid IdClient;
    private int IdInsuranceType;
    private int IdStatus;
    private int IdPaymentMethod;
    private decimal Premium;
    private string? Notes;
    private DateOnly StartAt;
    private DateOnly EndAt;

    public ProposalBuilder()
    {
        IdStatus = (int)EProposalStatus.Analyzing;
    }

    public ProposalBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    public ProposalBuilder WithIdStatus(int idStatus)
    {
        IdStatus = idStatus;
        return this;
    }

    public override Proposal Build()
    {
        var model = Proposal.Create(
            IdClient,
            IdInsuranceType,
            IdPaymentMethod,
            Premium,
            Notes,
            StartAt,
            EndAt);

        BuilderHelpers.SetProperty(model, nameof(Proposal.Id), Id);
        BuilderHelpers.SetProperty(model, nameof(Proposal.IdStatus), IdStatus);

        return model;
    }
}
