using ProposalService.Domain.Enums;

namespace ProposalService.UnitTest.Domain.Entities;

public class ProposalTests
{
    [Fact]
    public void Should_Create_With_Analysing_Status()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);

        var result = Proposal.Create(Guid.NewGuid(), 1, 2, 3, null, date, date);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal((int)EProposalStatus.Analyzing, result.IdStatus);
        });
    }
}
