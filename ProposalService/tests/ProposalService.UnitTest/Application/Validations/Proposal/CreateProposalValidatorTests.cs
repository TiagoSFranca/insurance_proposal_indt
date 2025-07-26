using ProposalService.Application.Validations.Proposal;

namespace ProposalService.UnitTest.Application.Validations.Proposal;

public class CreateProposalValidatorTests
{

    CreateProposalRequestBuilder _builder;

    CreateProposalValidator _sut;

    public CreateProposalValidatorTests()
    {
        _builder = new CreateProposalRequestBuilder();

        _sut = new CreateProposalValidator();
    }

    [Fact]
    public void EndDate_Should_Be_Greather_Than_Or_Equal_To_StartDate()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);

        var request = _builder
            .WithStartAt(date)
            .WithEndAt(date.AddYears(-11))
            .Build();

        var result = _sut.TestValidate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            result.ShouldHaveValidationErrorFor(e => e.EndAt);
        });
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Premium_Should_Be_Greather_Than_Or_Equal_To_Zero(decimal premium)
    {
        var date = DateOnly.FromDateTime(DateTime.Now);

        var request = _builder
            .WithPremium(premium)
            .Build();

        var result = _sut.TestValidate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            result.ShouldHaveValidationErrorFor(e => e.Premium);
        });
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var request = _builder
            .Build();

        var result = _sut.TestValidate(request);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsValid);
        });
    }
}
