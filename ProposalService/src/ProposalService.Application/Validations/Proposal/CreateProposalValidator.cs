using FluentValidation;

namespace ProposalService.Application.Validations.Proposal
{
    public class CreateProposalValidator : AbstractValidator<CreateProposalRequest>
    {
        public CreateProposalValidator()
        {
            RuleFor(e => e.EndAt)
                .GreaterThanOrEqualTo(e => e.StartAt);

            RuleFor(e => e.Premium)
                .GreaterThanOrEqualTo(0);
        }
    }
}
